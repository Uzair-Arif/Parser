using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Irony.Ast;
using Irony.Parsing;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Interpreter.Evaluator;
using Irony.Parsing.Construction;
using System.Text.RegularExpressions;
namespace IronyTest
{
     public partial class Form1 : Form
     {
          public Form1()
          {
               InitializeComponent();
          }
          public bool IsValidInputVar(string input)
          {
               string pattern = @"^[A-Za-z_][A-Za-z0-9]*$";
               return Regex.IsMatch(input, pattern);
          }
          public bool IsValidInputConstant(string input)
          {
               //string pattern = @"^[0-9][0-9]*$";
               string pattern = @"^[0-9]*\.?[0-9]$";
               return Regex.IsMatch(input, pattern);
          }
          public bool IsConstant(string input) 
          {
               string pattern = @"^[0-9][0-9]*$";
               return Regex.IsMatch(input, pattern);
          }
          public bool IsFloat(string input) 
          {
               string pattern = @"^[0-9]\.?[0-9]$";
               return Regex.IsMatch(input, pattern);
          }
          [Language("Expression", "1.0", "Dynamic geometry expression evaluator")]
          public class ExpressionGrammar : Irony.Parsing.Grammar
          {
               public ExpressionGrammar()
               {
                    
                    //// 1. Terminals
                    //Terminal number = new NumberLiteral("number");
                    Terminal identifier = new IdentifierTerminal("identifier");
                    Terminal number = new NumberLiteral("number");
                    //Terminal begin= new Terminal("begin");
                    //Terminal end = new Terminal("end");
                    //Terminal then = new Terminal("then");
                    //Terminal For = new Terminal("For");
                    //Terminal Else = new Terminal("else");
                    //Terminal If = new Terminal("if");
                    //Terminal Int = new Terminal("int");
                    //Terminal Char = new Terminal("char");
                    //Terminal print = new Terminal("print");
                    //Terminal equal = new Terminal("=");
                    //Terminal incrementop = new Terminal("++");
                    //Terminal staric = new Terminal("*");
                    //Terminal decrementop = new Terminal("--");
                    //Terminal semicolon = new Terminal(";");
                    //Terminal Lbrace = new Terminal("(");
                    //Terminal Rbrace = new Terminal(")");
                    //Terminal Lbracket = new Terminal("{");
                    //Terminal Rbracket = new Terminal("}");
                    //Terminal epsilon = new Terminal("empty");
                    //Terminal lessthen = new Terminal("<");
                    //Terminal greaterthen = new Terminal(">");
                    //Terminal vara = new Terminal("a");
                    //Terminal varb = new Terminal("b");
                    //Terminal constone = new Terminal("1");
                    //Terminal constthree = new Terminal("3");

                    //// 2. Non-terminals
                    NonTerminal Stmt = new NonTerminal("Stmt");
                    NonTerminal Dec = new NonTerminal("Dec");
                    NonTerminal Datattype = new NonTerminal("Datatype");
                    NonTerminal Con = new NonTerminal("Condition");
                    //NonTerminal Var = new NonTerminal("Var");
                    NonTerminal Const = new NonTerminal("Const");
                    NonTerminal Printstmt = new NonTerminal("Print-Stmt");
                    NonTerminal Variables = new NonTerminal("Variable");
                    NonTerminal Program = new NonTerminal("Program");
                    NonTerminal Declarationlist = new NonTerminal("Declaration-list");
                    NonTerminal condstmt = new NonTerminal("condstmt");
                    NonTerminal loopstmt = new NonTerminal("LoopStmt");
                    NonTerminal multistsmt = new NonTerminal("Multiply-Stmt");
                    NonTerminal valupdate = new NonTerminal("Value-Updation");
                   
                    
                   
                    ////3. BNF Rules
                    Program.Rule =ToTerm("begin()") + "{" + Stmt + "}" + "end";
                    //Stmt.Rule = identifier + "<" + identifier + ToTerm("=") + identifier + ";" | identifier + ">" + identifier + ToTerm("=") + identifier + ";" | ToTerm("if") + "(" + identifier + ToTerm("=") + identifier + ")" + "then" + Stmt + ToTerm("else") + Stmt | ToTerm("while") + "(" + identifier + ToTerm("=") + identifier + ")" + Stmt | ToTerm("do") + Stmt + ToTerm("while") + "(" + identifier + ToTerm("=") + identifier + ")" + ";" | "{" + Stmt + "}";
                    Stmt.Rule = Declarationlist + Stmt | condstmt + Stmt | Printstmt + Stmt | loopstmt + Stmt | multistsmt + Stmt | Empty; //| loopstmt + Stmt | multistsmt + Stmt | Printstmt + Stmt | Empty;
                    Declarationlist.Rule = Declarationlist + Dec | Dec;
                    condstmt.Rule = ToTerm("if") + "(" + Con + ")" + "then" + "{" + Stmt + "}" | ToTerm("if") + "(" + Con + ")" + "then" + "{" + Stmt + "}" + "else" + "{" + Stmt + "}";
                    loopstmt.Rule = ToTerm("for") + "(" + Declarationlist + ";" + Con + ";" + valupdate + ")" + "{" + Stmt + "}";
                    Con.Rule = Variables + "<" + Const | Variables + ">" + Const;
                    valupdate.Rule = Variables + ToTerm("++") | Variables + "--";
                    multistsmt.Rule = Variables + ToTerm("=") + Variables + ToTerm("*") + Variables + ";" | Variables + ToTerm("=") + Variables + ToTerm("*") + Const + ToTerm(";");
                   Dec.Rule = Datattype + Variables + ToTerm(";") | Datattype + Variables + "=" + Const + ";" | Variables + "=" + Const;
                    Datattype.Rule = ToTerm("int") | "char"|"float";
                    Printstmt.Rule = ToTerm("print") + Variables + ";" | ToTerm("print") + Const + ";";
                    Variables.Rule = identifier;
                    Const.Rule = number;
                   // Dec.Rule = Datattype + Def + ";";
                    
                    //Def.Rule = identifier | identifier + "=" + number;

                   // Datattype.Rule = ToTerm("int") | "char" | "float";

                    this.Root = Program;
               }
          }

          private void Form1_Load(object sender, EventArgs e)
          {

               //richTextBox3.Visible = false;
               listBox1.Visible = false;
               label3.Visible = false;
               //ExpressionGrammar ex = new ExpressionGrammar(); 
               //Parser parser = new Parser(ex);
               //ParseTree parseTree = parser.Parse("while(a=b){if(a=c) then id<c=u; else id>u=d;}");
               //ParseTreeNode root = parseTree.Root;
               //if (!parseTree.HasErrors())
               //{
                   
               //     MessageBox.Show(parseTree.Status.ToString());
                   
               //    dispTree(root, 0);
                    
               //}
               //else 
               //{
               //     MessageBox.Show("Unable to Parse"); 
               //}
          }
          public void dispTree(ParseTreeNode node, int level)
          {
               for (int i = 0; i < level; i++)

                    richTextBox1.Text += " ";
               richTextBox1.Text += "\n";
               richTextBox1.Text += node.ToString();

               foreach (ParseTreeNode child in node.ChildNodes)
                    dispTree(child, level + 1);
          }
          public void checkDec() 
          {
          }
          private void button1_Click(object sender, EventArgs e)
          {
               CreateSymboltable();
               richTextBox1.Text = string.Empty;
               listBox1.Items.Clear();
               ExpressionGrammar ex = new ExpressionGrammar();
               Parser parser = new Parser(ex);
               bool available=false;
               bool savailable = false;
               bool forcheck = false;
               bool sematic = false;
               bool declared = false;
               List<string> semanticcheck = new List<string>();
               List<string> variables = new List<string>();
               for ( int l = 0; l < richTextBox2.Lines.Length; l++)
               {
                  string[] sp = richTextBox2.Lines[l].Split('(', ')', '<', '>', '=', '\n', '\r', ' ',';');
                    if (sp[0] != "int" && sp[0] != "float")
                    {
                         foreach (string a in sp)
                         {
                              if (sp[0] == "for" && forcheck == true)
                                   break;
                              if (IsValidInputVar(a) && a != "while" && a != "if" && a != "then" && a != "else"&&a!="begin"&&a!="print"&&a!="for"&&a!="end" &&a!="If")
                              {
                                   //MessageBox.Show(a);
                                   foreach (DataGridViewRow ro in dataGridView1.Rows)
                                   {
                                        available = false;
                                        savailable = false;
                                        string cell = Convert.ToString(ro.Cells["var"].Value);
                                        if (cell == a)
                                        {
                                             available = true;
                                             //MessageBox.Show(a);
                                             foreach (string c in sp)
                                             {
                                                  savailable = false;
                                                  if ((IsConstant(c) && IsConstant(Convert.ToString(ro.Cells["value"].Value))) || (IsFloat(c) && IsFloat(Convert.ToString(ro.Cells["value"].Value))))
                                                  {


                                                       //MessageBox.Show(c);
                                                       savailable = true;
                                                       //break;

                                                  }
                                                  else
                                                  {
                                                       if (IsValidInputConstant(c) && savailable == false)
                                                       {
                                                            semanticcheck.Add("Please Verify Type of Identifier: "+a+" at line# "+l);
                                                            //MessageBox.Show("mismatch" + c);
                                                            savailable = false;
                                                            //break;
                                                       }

                                                  }
                                             }
                                             break;

                                        }

                                   }
                                   if (available == false)
                                   {
                                        variables.Add("Identifier "+a+" at line number :"+l+" is never declared");
                                        //MessageBox.Show(a);
                                      //error=new string[a];
                                   }
                                   forcheck = true;
                              }

                         }
                    }
               }
               if (variables.Count >= 1)
               {
                    declared = true;
                    //richTextBox3.Visible = true;
                    listBox1.Visible = true;
                    label3.Visible = true;
                    MessageBox.Show("Cannot Parse some variables may not be declared");
                    //richTextBox3.Lines = variables.ToArray();
                    foreach(string li in variables)
                    {listBox1.Items.Add(li);}
                    //listBox1.DataSource= variables;
               }
               if(semanticcheck.Count>=1)
               {
                    sematic = true;
                    listBox1.Visible = true;
                    //richTextBox3.Visible = true;
                    label3.Visible = true;
                    MessageBox.Show("Cannot Parse some variables type mismatch");
                    //richTextBox3.Lines = semanticcheck.ToArray();
                    foreach (string li in semanticcheck)
                    {
                         listBox1.Items.Add(li);
                    }
                    //listBox1.DataSource = semanticcheck;
               
               }
               if(declared==false&&sematic==false)
               {
                    
                              ParseTree parseTree = parser.Parse(richTextBox2.Text);
                              ParseTreeNode root = parseTree.Root;
                              if (!parseTree.HasErrors())
                              {

                                   MessageBox.Show(parseTree.Status.ToString());

                                   dispTree(root, 0);

                              }
                              else
                              {
                                   MessageBox.Show("Unable to Parse Verify your Grammar and Source Program");
                              }
                         
                    
               }
          }

          private void button2_Click(object sender, EventArgs e)
          {
              
               bool savailable = false;
               bool forcheck = false;
               //List<string> variables = new List<string>();
               List<string> semanticcheck = new List<string>();
               for (int l = 0; l < richTextBox2.Lines.Length; l++)
               {
                    string[] sp = richTextBox2.Lines[l].Split('(', ')', '<', '>', '=', '\n', '\r', ' ', ';');
                    if (sp[0] != "int" && sp[0] != "float")
                    {
                         foreach (string a in sp)
                         {
                              if (sp[0] == "for" && forcheck == true)
                                   break;
                              
                              if (IsValidInputVar(a) && a != "while" && a != "if" && a != "then" && a != "else" && a != "begin" && a != "print" && a != "for" && a != "end" && a != "If")
                              {
                                   foreach (DataGridViewRow ro in dataGridView1.Rows)
                                   {
                                       
                                        savailable = false;
                                        string cell = Convert.ToString(ro.Cells["var"].Value);
                                        if (cell == a)
                                        {
                                             foreach (string c in sp)
                                             {
                                                  savailable = false;
                                                  if ((IsConstant(c)&&IsConstant(Convert.ToString(ro.Cells["value"].Value)))||(IsFloat(c)&&IsFloat(Convert.ToString(ro.Cells["value"].Value)))) 
                                                  {
                                                      
                                                            
                                                            //MessageBox.Show(c);
                                                            savailable = true;
                                                            //break;
                                                       
                                                  }
                                                  else
                                                  {
                                                       if (IsValidInputConstant(c)&&savailable==false)
                                                       {
                                                            //semanticcheck.Add(c);
                                                            MessageBox.Show("mismatch"+c);
                                                            savailable = false;
                                                            //break;
                                                       }
                                                       
                                                  }
                                             }
                                             //available = true;
                                             //MessageBox.Show(a);
                                             //break;
                                        }

                                   }
                                   //if (available == false)
                                   //{
                                   //     variables.Add("Identifier " + a + " at line number :" + l + " is never declared");
                                   //     //MessageBox.Show(a);
                                   //     //error=new string[a];
                                   //}
                                   forcheck = true;
                              }

                         }
                    }
               }
               //ExpressionGrammar ex = new ExpressionGrammar();
               //Parser parser = new Parser(ex);
               //ParseTree parseTree = parser.Parse(richTextBox2.Text);
               //ParseTreeNode root = parseTree.Root;
               //if (!parseTree.HasErrors())
               //{

               //     MessageBox.Show(parseTree.Status.ToString());

               //     dispTree(root, 0);

               //}
               //else
               //{
               //     MessageBox.Show("Unable to Parse");
               //}
                         
          }

         

          private void button3_Click(object sender, EventArgs e)
          {
               //dataGridView1.DataSource = null;
               //dataGridView1.Rows.Clear();
               //DataTable dt = new DataTable();
               //dt.Columns.Add("var");
               //dt.Columns.Add("data type");
               //dt.Columns.Add("value");
               //dt.Columns.Add("line #");
               
               
               //for (int i = 0; i < richTextBox2.Lines.Length; i++)
               //{

               //     string[] eachLine = richTextBox2.Lines[i].Split('\n', ';', ' ','=','(',')');

               //     if (eachLine[0] == "int" || eachLine[0] == "float")
               //     {
               //          // Get the lines of text.
               //         // string[] lineArray = richTextBox2.Lines;

               //          // Create a collection so that a line can be removed.
               //          //var lineCollection = new List<string>(lineArray);

               //          // Remove the desired line.
               //          //lineCollection.RemoveAt(i);

               //          // Convert the collection back to an array.
               //          //lineArray = lineCollection.ToArray();

               //          //richTextBox2.Lines = lineArray;
                        
               //          DataRow dr = dt.NewRow();
               //          bool v = false;
               //          foreach (string s in eachLine)
               //          {

               //               if (IsValidInputVar(s))
               //               {
               //                    if (s != "int" && s != "float")
               //                    {
               //                         if (dataGridView1.Rows.Count >= 1)
               //                         {
               //                              foreach (DataGridViewRow ro in dataGridView1.Rows)
               //                              {

               //                                   string cell = Convert.ToString(ro.Cells["var"].Value);
               //                                   //string spcell = cell.Split(' ').First();
               //                                   if (cell == s)
               //                                   {

               //                                        ro.Cells["value"].Value = eachLine[3];
               //                                        //ro.Cells["data type"].Value = "Error";
               //                                        v = true;

               //                                   }

               //                              }
               //                         }

               //                         if (v == false)
               //                              dr["var"] = s;

               //                    }

               //               }
               //               if (IsValidInputConstant(s))
               //               {
               //                    if (v == false)
               //                         dr["value"] = s;
               //                    // dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count-2);
               //               }

               //               if (s == "int" || s == "float")
               //               {
               //                    if (v == false)
               //                         dr["data type"] = s;
               //               }
               //               if (v == false)
               //                    dr["line #"] = i;

                              
               //          }
               //          dt.Rows.Add(dr);
               //          dataGridView1.DataSource = dt;
                         
                         
               //     }
                    
               //}
              
          }
          public void CreateSymboltable() 
          {
               dataGridView1.DataSource = null;
               dataGridView1.Rows.Clear();
               DataTable dt = new DataTable();
               dt.Columns.Add("var");
               dt.Columns.Add("data type");
               dt.Columns.Add("value");
               dt.Columns.Add("line #");


               for (int i = 0; i < richTextBox2.Lines.Length; i++)
               {

                    string[] eachLine = richTextBox2.Lines[i].Split('\n', ';', ' ', '=', '(', ')');

                    if (eachLine[0] == "int" || eachLine[0] == "float")
                    {
                         // Get the lines of text.
                         // string[] lineArray = richTextBox2.Lines;

                         // Create a collection so that a line can be removed.
                         //var lineCollection = new List<string>(lineArray);

                         // Remove the desired line.
                         //lineCollection.RemoveAt(i);

                         // Convert the collection back to an array.
                         //lineArray = lineCollection.ToArray();

                         //richTextBox2.Lines = lineArray;

                         DataRow dr = dt.NewRow();
                         bool v = false;
                         foreach (string s in eachLine)
                         {

                              if (IsValidInputVar(s))
                              {
                                   if (s != "int" && s != "float")
                                   {
                                        if (dataGridView1.Rows.Count >= 1)
                                        {
                                             foreach (DataGridViewRow ro in dataGridView1.Rows)
                                             {

                                                  string cell = Convert.ToString(ro.Cells["var"].Value);
                                                  //string spcell = cell.Split(' ').First();
                                                  if (cell == s)
                                                  {

                                                       ro.Cells["value"].Value = eachLine[3];
                                                       //ro.Cells["data type"].Value = "Error";
                                                       v = true;

                                                  }

                                             }
                                        }

                                        if (v == false)
                                             dr["var"] = s;

                                   }

                              }
                              if (IsValidInputConstant(s))
                              {
                                   if (v == false)
                                        dr["value"] = s;
                                   // dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count-2);
                              }

                              if (s == "int" || s == "float")
                              {
                                   if (v == false)
                                        dr["data type"] = s;
                              }
                              if (v == false)
                                   dr["line #"] = i;


                         }
                         dt.Rows.Add(dr);
                         dataGridView1.DataSource = dt;


                    }

               }
           
          }
          private void label1_Click(object sender, EventArgs e)
          {

          }

          
     }
}
