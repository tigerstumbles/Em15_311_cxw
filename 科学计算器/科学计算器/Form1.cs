using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Threading;



namespace 科学计算器
{
    public partial class Form1 : Form
    {
        string introduction = "null";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {           

//***********************用eval()函数实现时使用该语句******************************************
            //cal.Language = "javascript";   
//*********************************************************************************************

        }

        

        #region 操作符按钮
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "+";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "-";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "*";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "/";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "(";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += ")";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            textBox1.Text += "π";
            //textBox1.Text += "Math.PI";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            textBox1.Text += "e";
            //textBox1.Text += "Math.E";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "^";
            //textBox1.Text += "Math.pow(";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "√";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text += "!";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text += "s";
            //textBox1.Text += "Math.sin(";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text += "C";
            //textBox1.Text += "Math.cos(";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text += "t";
            //textBox1.Text += "Math.tan(";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text += "c";
            //textBox1.Text += "Math.cot(";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text += "L";
            //textBox1.Text += "Math.log10(";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text += "l";
            //textBox1.Text += "Math.log(";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "m";
            //textBox1.Text += "%";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += "%";
            //textBox1.Text += "*0.01";         
        }

        private void button21_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button22_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
        }

        #endregion

        #region 数字按钮

        private void button31_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
        }

        private void button23_Click(object sender, EventArgs e)
        {
            textBox1.Text += "1";
        }

        private void button24_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
        }

        private void button25_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
        }

        private void button26_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
        }

        private void button27_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
        }

        private void button28_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
        }

        private void button29_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
        }

        private void button30_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
        }

        private void button32_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
        }

        private void button33_Click(object sender, EventArgs e)
        {
            textBox1.Text += ".";
        }

        private void button34_Click(object sender, EventArgs e)
        {
            textBox1.Text += "@";
        }

        private void button35_Click(object sender, EventArgs e)
        {
            textBox1.Text += '~';
        }

        #endregion

        #region 辅助函数

        //*************************************************************************************
        //定义一个函数，用于获取各操作符的优先级
        private int getRank(char s)            
        {
            int rank = 0;
            switch (s)
            {
                case '#': rank = 0; break;
                case '(': rank = 1; break;
                case '+': rank = 2; break;
                case '-': rank = 2; break;
                case '*': rank = 3; break;
                case '/': rank = 3; break;
                case 'm': rank = 3; break; //即为mod
                case 's': rank = 3; break;
                case 'C': rank = 3; break;
                case 't': rank = 3; break;
                case 'c': rank = 3; break;
                case 'L': rank = 3; break;
                case 'l': rank = 3; break;
                case '√': rank = 3; break; //开方
                case '@': rank = 4; break;  //正
                case '~': rank = 4; break;  //负
                case '!': rank = 5; break; //阶乘
                case '%': rank = 5; break; //百分号
                case '^': rank = 5; break;
                case ')': rank = 6; break;
                default: rank = -1; break;
            }
            return rank;
        }

        //***************************************************************************************
        //用于检查符号栈顶有单目运算符时的情况
        private void hasSingleOpr( Queue<string> stored_number, Stack<char> stored_char) 
        {
            if (stored_char.Count != 0)
            {
                char d = stored_char.Peek();
                if (d == '√' || d == 's' || d == 'C' || d == 't' || d == 'c' || d == 'L' || d == 'l' || d == '!' || d == '%')
                {
                    do                                       
                    {                                        
                        d = stored_char.Pop();
                        stored_number.Enqueue(char.ToString(d));
                        if (stored_char.Count != 0)
                            d = stored_char.Peek();
                        else
                            break;
                    } while (d == '√' || d == 's' || d == 'C' || d == 't' || d == 'c' || d == 'L' || d == 'l' || d == '!' || d == '%');
                }

            }
        }

        #endregion

//***********************用eval()函数实现时使用下面部分语句*************************************
        ////MSScriptControl.ScriptControlClass Cal = new MSScriptControl.ScriptControlClass();
        //MSScriptControl.ScriptControl cal = new MSScriptControl.ScriptControl(); 
//**********************************************************************************************

        #region 按下"="时对表达式进行计算

        private void button15_Click(object sender, EventArgs e)
        {
            Queue<string> stored_number = new Queue<string>(); //创建一个存储器用于存放数据；
            Stack<char> stored_char = new Stack<char>();       //创建一个符号栈用于存放运算符；
            string expression = textBox1.Text, temp = "";
            expression += "#";                                 //"#"用于标志表达式的结束
            double result = 0;
            int i = 0;
            char a = expression[i];
            while (i < expression.Length)//将表达式转换为后序波兰式以便于计算
            {
                if (a == '@' || a == '~' || Char.IsNumber(a) || a == '.')
                {
                    if (a == '@' || a == '~')                       //将正负号直接压入符号栈
                    {
                        stored_char.Push(a);
                        a = expression[++i];
                        if (!Char.IsNumber(a) && a != 'π' && a != 'e'&&a!='(')
                        {
                            MessageBox.Show("正负号后面不能是运算符！");
                            break;
                        }

                    }
                    if (Char.IsNumber(a) || a == '.')
                    {
                        temp = "";
                        while (Char.IsNumber(a) || a == '.')     //一：当读出为数据时：
                        {                                           //    1.将数据暂时保存在字符串temp中
                            temp += a;
                            i++;
                            a = expression[i];
                        }
                        stored_number.Enqueue(temp);            //2.将数据写入存储器
                        hasSingleOpr(stored_number, stored_char);//3.检查符号栈顶是否有单目运算符，有的话全部出栈并写入存储器
                    }                    
                }

                //textBox2.Text = stored_number.Peek().ToString();

                if (a == 'π')
                {
                    stored_number.Enqueue("π");
                    hasSingleOpr( stored_number,  stored_char);//3.检查符号栈顶是否有单目运算符，有的话全部出栈并写入存储器
                }
                if (a == 'e')
                {
                    stored_number.Enqueue("e");
                    hasSingleOpr( stored_number,  stored_char);//3.检查符号栈顶是否有单目运算符，有的话全部出栈并写入存储器
                }

               
                if (a == '+' || a == '-' || a == '*' || a == '/' || a == '^' || a == 'm')  //二：当读出为双目运算符时，大于栈顶优先级，则入栈；
                {                                                   //否则弹出栈顶符号并写入数据栈，直到栈顶符号的运算优先级较小为止
                    if (stored_char.Count == 0)                     //若栈为空，直接读入
                        stored_char.Push(a);
                    else
                    {
                        if (getRank(a) > getRank(stored_char.Peek()))
                            stored_char.Push(a);
                        else
                        {

                            while (getRank(a) <= getRank(stored_char.Peek()))
                            {
                                char b = stored_char.Pop();
                                stored_number.Enqueue(char.ToString(b));
                                if (stored_char.Count == 0)
                                {
                                    break;
                                }
                            }
                            stored_char.Push(a);

                        }
                    }
                }
                else if (a == '(' || a == '√' || a == 's' || a == 'C' || a == 't' || a == 'c' || a == 'L' || a == 'l'||a=='@'||a=='~')
                    stored_char.Push(a);            //三：如果是左单目运算符,左括号或者函数名，直接入符号栈
                else if (a == '!' || a == '%')     //四：如果是右单目运算符，直接入存储器栈
                    stored_number.Enqueue(char.ToString(a));
                else if (a == ')')                    //五：如果是右括号")"，则弹出符号栈数据，写入存储器，
                {                                     //一直到左括号弹出(左括弧直接丢弃，不写入存储器)，
                    char b = stored_char.Pop();        //再检查栈顶是否为左单目运算符或者函数名，
                    while (b != '(')                    //是的话继续弹出，直到遇到双目运算符；
                    {
                        stored_number.Enqueue(char.ToString(b));
                        b = stored_char.Pop();
                    }
                    if (stored_char.Count == 0)
                    {
                        a = expression[++i];
                        continue;
                    }
                    do
                    {
                        b = stored_char.Pop();
                        if (b == '(' || b == '√' || b == 's' || b == 'C' || b == 't' || b == 'c' || b == 'L' || b == 'l'||b=='@'||b=='~')
                        {
                            stored_number.Enqueue(char.ToString(b));
                        }
                    } while ((b != '+' && b != '-' && b != '*' && b != '/' && b != '^' && b != 'm') && stored_char.Count != 0);
                    if (b == '+' || b == '-' || b == '*' || b == '/' || b == '^' || b == 'm')
                        stored_char.Push(b);
                }
                if (a == '#')
                    break;
                else
                {
                    i++;
                    a = expression[i];
                }
            }//将表达式转换为后序波兰式结束
            //  textBox2.Text = stored_number.Dequeue().ToString() ;


            char n = 'a';                          //此时读出字符为#，说明表达式已全部读完
            while (stored_char.Count > 0)          //则符号栈全部弹出并写入存储器
            {
                n = stored_char.Pop();
                stored_number.Enqueue(char.ToString(n));
            }


            Stack<double> mediate = new Stack<double>(); //中间值存储栈用于存放计算结果
            string s = "0";
            double p = 0;
            while (stored_number.Count > 0)
            {
                s = stored_number.Dequeue();
                if (char.IsDigit(s, 0))    //若读出为数据，直接将数据压入中间值存储栈
                {
                    p = Double.Parse(s);
                    mediate.Push(p);
                }
                if (s == "π")
                {
                    mediate.Push(Math.PI);
                }
                if (s == "e")
                {
                    mediate.Push(Math.E);
                }
                if (s == "√" || s == "s" || s == "C" || s == "t" || s == "c" || s == "L" || s == "l" || s == "!" || s == "%"||s=="@"||s=="~")
                {
                    double q = mediate.Pop();                    //若为单目运算符及函数
                    try
                    {
                        switch (s)
                        {
                            case "√": q = Math.Sqrt(q); break;
                            case "s": q = Math.Sin(q); break;
                            case "C": q = Math.Cos(q); break;
                            case "t": q = Math.Tan(q); break;
                            case "c": q = 1 / Math.Tan(q); break;
                            case "L": q = Math.Log10(q); break;
                            case "l": q = Math.Log(q); break;
                            case "!":
                                if (q < 0)
                                    MessageBox.Show("阶乘对象必须为非负整数！");
                                else                         //注：因为无法精确地判断double是否正好为一个正整数
                                {                            //(估计是内存的关系，如把5存入stored_number，它在里
                                    int r = 1;               // 面的值不会恰恰为5.00)所以只能把带小数的也一视同
                                    for (int t = 1; t <= q; t++)    //仁为整数来计算阶乘.下面的取余也是同理.
                                    {
                                        r *= t;
                                    }
                                    q = r;
                                }
                                break;
                            case "%": q = q * 0.01; break;
                            case "@": q = Math.Abs(q); break;
                            case "~": q = 0 - q; break;
                            default: break;
                        }// switch结束
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());                 
                    }

                    mediate.Push(q);
                }
                if (s == "+" || s == "-" || s == "*" || s == "/" || s == "^" || s == "m") //若为双目运算符
                {
                    double c = mediate.Pop();
                    double d = mediate.Pop();
                    try
                    {
                        switch (s)
                        {
                            case "+": c = c + d; break;
                            case "-": c = d - c; break;
                            case "*": c = c * d; break;
                            case "/": c = d / c; break;
                            case "^": c = Math.Pow(d, c); break;
                            case "m": c = d % c; break;
                            default:
                                break;
                        }//switch结束
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    mediate.Push(c);
                }
            }
            result = mediate.Pop();               //中间值栈留存的最后一个数即为结果
            string resultstr = result.ToString();
            textBox2.Text = resultstr;
        }

        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

//***********************用eval()函数实现时使用下面部分语句************************************
        /* string formula = textBox1.Text;
         object result=0;
         try
         {
             result = cal.Eval(formula);
                
         }
         catch (Exception ex)
         {
             MessageBox .Show("表达式错误：" + ex.Message);
             result = "";
         }
         textBox2.Text = result.ToString();*/
//*********************************************************************************************



    }
}
    
        

