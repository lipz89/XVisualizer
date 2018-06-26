using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringVisualizer.Jsons.XJson
{
    class XJReader : IDisposable
    {
        private static Regex nameRegex = new Regex("^[$_A-Za-z][$_A-Za-z0-9]*$");

        private enum JNode
        {
            None,
            Array,
            Object,
            Quote,
            Value
        }

        private TextReader _reader;
        private Stack<JNode> _stack;
        private char _currentChar;
        private char _quoteChar;
        private int _line, _position;
        private bool _hasMoreToken;

        public XJReader(string input)
        {
            _reader = new StringReader(input);
            _stack = new Stack<JNode>();
            _stack.Push(JNode.None);
            _line = 1;
            _position = 1;
        }

        public XJToken Read()
        {
            var token = ReadToken();
            if (SkipWhiteSpace())
            {
                if (HasNext())
                {
                    MoveNext();
                    throw Exception("多余字符:" + _currentChar, _line, _position - 1);
                }
            }
            else if (_currentChar != '\0')
            {
                throw Exception("多余字符:" + _currentChar, _line, _position - 1);
            }
            var node = this._stack.Pop();
            if (this._stack.Any() || node != JNode.None)
            {
                throw Exception("解析错误");
            }
            token.IsRoot = true;
            return token;
        }

        private XJToken ReadToken()
        {
            XJToken token = null;
            SkipWhiteSpace();
            if (_currentChar == '{' || _currentChar == '[')
            {
                if (_currentChar == '{')
                {
                    this._stack.Push(JNode.Object);
                    token = ReadObject();
                }
                else
                {
                    this._stack.Push(JNode.Array);
                    token = ReadArray();
                }
                this._stack.Pop();

                CheckEnd();
            }
            else if (_currentChar == '\"' || _currentChar == '\'')
            {
                this._quoteChar = _currentChar;
                this._stack.Push(JNode.Quote);

                var value = ReadString();
                token = new XJString(value);
                this._stack.Pop();

                CheckEnd();
            }
            else
            {
                this._stack.Push(JNode.Value);
                string txt = _currentChar.ToString();
                bool flag;
                while (flag = this.MoveNext())
                {
                    if (IsWhite() || IsEnd())
                    {
                        break;
                    }
                    txt += _currentChar;
                }
                this._stack.Pop();
                if (!flag && this._stack.Count > 1)
                {
                    throw Exception("意外的结束");
                }
                token = ParseValue(txt);
            }
            if (token != null)
            {
                return token;
            }
            throw Exception("解析错误");
        }

        private bool IsWhite()
        {
            return this._currentChar == ' ' || this._currentChar == '\r' || this._currentChar == '\n' ||
                   this._currentChar == '\t';
        }

        private bool IsEnd()
        {
            return this._currentChar == ',' ||
                   (this._currentChar == ']' /*&& this._stack.Peek() == JNode.Array*/) ||
                   (this._currentChar == '}' /* && this._stack.Peek() == JNode.Object*/);
        }

        private XJObject ReadObject()
        {
            var obj = new XJObject
            {
                Children = new List<XJToken>()
            };
            while (MoveNext())
            {
                SkipWhiteSpace();
                if (_currentChar == '}')
                {
                    if (_hasMoreToken)
                    {
                        throw Exception("对象意外的结束：" + _currentChar, _line, _position - 1);
                    }
                    _hasMoreToken = false;
                    break;
                }
                _hasMoreToken = false;
                string name = null;
                if (_currentChar == '\"' || _currentChar == '\'')
                {
                    this._quoteChar = _currentChar;
                    this._stack.Push(JNode.Quote);

                    name = ReadString();

                    this._stack.Pop();

                    var l = _line;
                    var p = _position;
                    var nn = ReadTo(':');
                    if (nn.Trim().Length != 0)
                    {
                        throw Exception("未能解析的字符：" + nn, l, p);
                    }
                }
                else
                {
                    var l = _line;
                    var p = _position;
                    name = (_currentChar + ReadTo(':')).Trim();
                    if (!nameRegex.IsMatch(name))
                    {
                        throw Exception("名称不规范：" + name, l, p);
                    }
                }
                if (!MoveNext())
                {
                    throw Exception("意外的结束");
                }
                var token = ReadToken();
                if (token != null)
                {
                    token.Name = name;
                    token.Parent = obj;
                    obj.Children.Add(token);
                }
                SkipWhiteSpace();
                if (_currentChar == '}')
                {
                    break;
                }
                if (_currentChar != ',')
                {
                    throw Exception("未能解析的字符：" + _currentChar, _line, _position - 1);
                }
                _hasMoreToken = true;
            }
            return obj;
        }

        private XJArray ReadArray()
        {
            var array = new XJArray()
            {
                Children = new List<XJToken>()
            };
            while (MoveNext())
            {
                SkipWhiteSpace();
                if (_currentChar == ']')
                {
                    if (_hasMoreToken)
                    {
                        throw Exception("数组意外的结束：" + _currentChar, _line, _position - 1);
                    }
                    _hasMoreToken = false;
                    break;
                }
                _hasMoreToken = false;
                var token = ReadToken();
                if (token != null)
                {
                    token.Name = array.Children.Count.ToString();
                    token.Parent = array;
                    array.Children.Add(token);
                }
                SkipWhiteSpace();
                if (_currentChar == ']')
                {
                    break;
                }
                if (_currentChar != ',')
                {
                    throw Exception("未能解析的字符：" + _currentChar, _line, _position - 1);
                }
                _hasMoreToken = true;
            }
            return array;
        }

        private XJValue ParseValue(string sj)
        {
            sj = sj.Trim();
            if (sj == "null")
            {
                return new XJValue(null);
            }
            if (sj == "undefined")
            {
                return new XJValue("undefined");
            }
            if (sj == "true")
            {
                return new XJValue(true);
            }
            if (sj == "false")
            {
                return new XJValue(false);
            }
            if (!sj.Contains('.'))
            {
                int i;
                if (int.TryParse(sj, out i))
                {
                    return new XJValue(i);
                }
                long l;
                if (long.TryParse(sj, out l))
                {
                    return new XJValue(l);
                }
            }
            else
            {
                double d;
                if (double.TryParse(sj, out d))
                {
                    return new XJValue(d);
                }
            }
            throw Exception("不是一个合法的值：" + sj, _line, _position - 1);
        }

        private void CheckEnd()
        {
            if (!MoveNext())
            {
                if (this._stack.Count > 1)
                {
                    throw Exception("意外的结束");
                }
                else
                {
                    this._currentChar = '\0';
                }
            }
        }

        private bool MoveNext()
        {
            int num = this._reader.Read();
            if (num != -1)
            {
                this._currentChar = (char)num;
                if (_currentChar == '\n')
                {
                    _line++;
                    _position = 1;
                }
                else
                {
                    _position++;
                }
                return true;
            }
            this._currentChar = '\0';
            return false;
        }

        private bool HasNext()
        {
            return this._reader.Peek() != -1;
        }

        private char PeekNext()
        {
            return (char)this._reader.Peek();
        }

        private void ClearCurrentChar()
        {
            this._currentChar = '\0';
        }

        private bool MoveTo(char value)
        {
            while (this.MoveNext())
            {
                if (this._currentChar == value)
                {
                    return true;
                }
            }
            return false;
        }

        private string ReadTo(char value)
        {
            string txt = string.Empty;
            while (this.MoveNext())
            {
                if (this._currentChar == value)
                {
                    return txt;
                }
                txt += _currentChar;
            }
            throw Exception("未读取到目标字符：" + value);
        }

        private string ReadString()
        {
            string txt = string.Empty;
            while (this.MoveNext())
            {
                if (this._currentChar == '\\')
                {
                    MoveNext();
                    if ("ubfnrt\'\"\\".Contains(_currentChar))
                    {
                        if (_currentChar == 'n')
                            txt += "\n";
                        else if (_currentChar == 'b')
                            txt += "\b";
                        else if (_currentChar == 'f')
                            txt += "\f";
                        else if (_currentChar == 'r')
                            txt += "\r";
                        else if (_currentChar == 't')
                            txt += "\t";
                        else if (_currentChar == '\'')
                            txt += "\'";
                        else if (_currentChar == '\"')
                            txt += "\"";
                        else if (_currentChar == '\\')
                            txt += "\\";
                        else if (_currentChar == 'u')
                            txt += ReadUnicode();
                    }
                    else
                    {
                        throw Exception("未能解析的转义字符：\\" + _currentChar, _line, _position - 1);
                    }
                }
                else if (this._currentChar != this._quoteChar)
                {
                    txt += _currentChar;
                }
                else
                {
                    return txt;
                }
            }
            throw Exception("未读取到目标字符：" + this._quoteChar);
        }

        private char ReadUnicode()
        {
            var l = _line;
            var p = _position;
            var str = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                MoveNext();
                if (IsWhite() || IsEnd())
                    break;

                str += _currentChar;
            }

            if (str.Length == 4)
            {
                try
                {
                    int charCode = Convert.ToInt32(str, 16);
                    return (char)charCode;
                }
                catch { }
            }

            throw Exception("无效的转义字符：\\u" + str, l, p);
        }

        private bool SkipWhiteSpace()
        {
            if (IsWhite() || _currentChar == '\0')
            {
                while (MoveNext())
                {
                    if (!IsWhite())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private Exception Exception(string msg, int? line = null, int? position = null)
        {
            return new Exception(string.Format("{0},行{1},位置{2}",
                msg,
                line ?? this._line,
                position ?? this._position));
        }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}