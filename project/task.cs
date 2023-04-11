
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF          =  0, // (EOF)
        SYMBOL_ERROR        =  1, // (Error)
        SYMBOL_WHITESPACE   =  2, // Whitespace
        SYMBOL_MINUS        =  3, // '-'
        SYMBOL_MINUSMINUS   =  4, // '--'
        SYMBOL_EXCLAMEQ     =  5, // '!='
        SYMBOL_EXCLAMFALSE  =  6, // '!false'
        SYMBOL_LPAREN       =  7, // '('
        SYMBOL_RPAREN       =  8, // ')'
        SYMBOL_TIMES        =  9, // '*'
        SYMBOL_TIMESEQ      = 10, // '*='
        SYMBOL_DIV          = 11, // '/'
        SYMBOL_DIVEQ        = 12, // '/='
        SYMBOL_COLON        = 13, // ':'
        SYMBOL_SEMI         = 14, // ';'
        SYMBOL_LBRACE       = 15, // '{'
        SYMBOL_RBRACE       = 16, // '}'
        SYMBOL_PLUS         = 17, // '+'
        SYMBOL_PLUSPLUS     = 18, // '++'
        SYMBOL_PLUSEQ       = 19, // '+='
        SYMBOL_LT           = 20, // '<'
        SYMBOL_LTEQ         = 21, // '<='
        SYMBOL_EQ           = 22, // '='
        SYMBOL_MINUSEQ      = 23, // '-='
        SYMBOL_GT           = 24, // '>'
        SYMBOL_GTEQ         = 25, // '>='
        SYMBOL_CASE         = 26, // case
        SYMBOL_ELSIF        = 27, // elsif
        SYMBOL_END          = 28, // End
        SYMBOL_ENDELSIF     = 29, // endelsif
        SYMBOL_ENDFOR       = 30, // endfor
        SYMBOL_ENDIF        = 31, // endif
        SYMBOL_ENDSWITCH    = 32, // endswitch
        SYMBOL_ENDWHILE     = 33, // endwhile
        SYMBOL_FLOAT        = 34, // Float
        SYMBOL_FOR          = 35, // for
        SYMBOL_ID           = 36, // ID
        SYMBOL_IDENTFIER    = 37, // identfier
        SYMBOL_IF           = 38, // if
        SYMBOL_INT          = 39, // int
        SYMBOL_INTEGER      = 40, // Integer
        SYMBOL_START        = 41, // Start
        SYMBOL_SWITCH       = 42, // switch
        SYMBOL_TRUE         = 43, // true
        SYMBOL_WHILE        = 44, // while
        SYMBOL_ADD          = 45, // <add>
        SYMBOL_ASSIGMENT    = 46, // <assigment>
        SYMBOL_CONTANT      = 47, // <contant>
        SYMBOL_DECLARATION  = 48, // <declaration>
        SYMBOL_DIV2         = 49, // <div>
        SYMBOL_EXP          = 50, // <exp>
        SYMBOL_FORAS        = 51, // <foras>
        SYMBOL_FORINCDEC    = 52, // <forincdec>
        SYMBOL_MUL          = 53, // <mul>
        SYMBOL_PROGRAM      = 54, // <program>
        SYMBOL_STATMENTLIST = 55, // <statmentlist>
        SYMBOL_SUB          = 56  // <sub>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_START_LBRACE_RBRACE_END                                                         =  0, // <program> ::= Start '{' <contant> '}' End
        RULE_CONTANT                                                                                 =  1, // <contant> ::= <assigment> <statmentlist>
        RULE_CONTANT2                                                                                =  2, // <contant> ::= <declaration> <statmentlist>
        RULE_CONTANT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE_ENDFOR                                =  3, // <contant> ::= for '(' <foras> ';' <exp> ';' <forincdec> ')' '{' <statmentlist> '}' endfor
        RULE_CONTANT_WHILE_LPAREN_RPAREN_LBRACE_RBRACE_ENDWHILE                                      =  4, // <contant> ::= while '(' <exp> ')' '{' <statmentlist> '}' endwhile
        RULE_CONTANT_SWITCH_LPAREN_IDENTFIER_RPAREN_LBRACE_CASE_INTEGER_COLON_RBRACE_ENDSWITCH       =  5, // <contant> ::= switch '(' identfier ')' '{' case Integer ':' <statmentlist> '}' endswitch
        RULE_CONTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ENDIF                                            =  6, // <contant> ::= if '(' <exp> ')' '{' <statmentlist> '}' endif
        RULE_CONTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ENDIF_ELSIF_LPAREN_RPAREN_LBRACE_RBRACE_ENDELSIF =  7, // <contant> ::= if '(' <exp> ')' '{' <statmentlist> '}' endif elsif '(' <exp> ')' '{' <statmentlist> '}' endelsif
        RULE_ASSIGMENT_IDENTFIER_END                                                                 =  8, // <assigment> ::= identfier End
        RULE_DECLARATION_IDENTFIER_EQ_INT_END                                                        =  9, // <declaration> ::= identfier '=' int End
        RULE_DECLARATION_IDENTFIER_EQ_FLOAT_END                                                      = 10, // <declaration> ::= identfier '=' Float End
        RULE_FORAS_IDENTFIER_EQ_INT                                                                  = 11, // <foras> ::= identfier '=' int
        RULE_FORAS_IDENTFIER                                                                         = 12, // <foras> ::= identfier
        RULE_FORINCDEC_IDENTFIER_PLUSPLUS                                                            = 13, // <forincdec> ::= identfier '++'
        RULE_FORINCDEC_IDENTFIER_MINUSMINUS                                                          = 14, // <forincdec> ::= identfier '--'
        RULE_FORINCDEC_PLUSPLUS_IDENTFIER                                                            = 15, // <forincdec> ::= '++' identfier
        RULE_FORINCDEC_MINUSMINUS_IDENTFIER                                                          = 16, // <forincdec> ::= '--' identfier
        RULE_FORINCDEC_IDENTFIER_PLUSEQ_INT                                                          = 17, // <forincdec> ::= identfier '+=' int
        RULE_FORINCDEC_IDENTFIER_MINUSEQ_INT                                                         = 18, // <forincdec> ::= identfier '-=' int
        RULE_FORINCDEC_IDENTFIER_TIMESEQ_INT                                                         = 19, // <forincdec> ::= identfier '*=' int
        RULE_FORINCDEC_IDENTFIER_DIVEQ_INT                                                           = 20, // <forincdec> ::= identfier '/=' int
        RULE_FORINCDEC_IDENTFIER_EQ_IDENTFIER_PLUS_INT                                               = 21, // <forincdec> ::= identfier '=' identfier '+' int
        RULE_FORINCDEC_IDENTFIER_EQ_IDENTFIER_MINUS_INT                                              = 22, // <forincdec> ::= identfier '=' identfier '-' int
        RULE_FORINCDEC_IDENTFIER_EQ_IDENTFIER_TIMES_INT                                              = 23, // <forincdec> ::= identfier '=' identfier '*' int
        RULE_FORINCDEC_IDENTFIER_EQ_IDENTFIER_DIV_INT                                                = 24, // <forincdec> ::= identfier '=' identfier '/' int
        RULE_EXP_IDENTFIER_LTEQ_INT                                                                  = 25, // <exp> ::= identfier '<=' int
        RULE_EXP_IDENTFIER_GTEQ_INT                                                                  = 26, // <exp> ::= identfier '>=' int
        RULE_EXP_IDENTFIER_GT_INT                                                                    = 27, // <exp> ::= identfier '>' int
        RULE_EXP_IDENTFIER_LT_INT                                                                    = 28, // <exp> ::= identfier '<' int
        RULE_EXP_IDENTFIER_EXCLAMEQ_INT                                                              = 29, // <exp> ::= identfier '!=' int
        RULE_EXP_TRUE                                                                                = 30, // <exp> ::= true
        RULE_EXP_EXCLAMFALSE                                                                         = 31, // <exp> ::= '!false'
        RULE_STATMENTLIST                                                                            = 32, // <statmentlist> ::= <contant> <statmentlist>
        RULE_STATMENTLIST2                                                                           = 33, // <statmentlist> ::= <add> <statmentlist>
        RULE_STATMENTLIST3                                                                           = 34, // <statmentlist> ::= <sub> <statmentlist>
        RULE_STATMENTLIST4                                                                           = 35, // <statmentlist> ::= <div> <statmentlist>
        RULE_STATMENTLIST5                                                                           = 36, // <statmentlist> ::= <mul> <statmentlist>
        RULE_STATMENTLIST6                                                                           = 37, // <statmentlist> ::= 
        RULE_ADD_IDENTFIER_EQ_IDENTFIER_PLUS_INT_END                                                 = 38, // <add> ::= identfier '=' identfier '+' int End
        RULE_ADD_IDENTFIER_PLUSEQ_INT_END                                                            = 39, // <add> ::= identfier '+=' int End
        RULE_ADD_IDENTFIER_EQ_IDENTFIER_PLUS_IDENTFIER_END                                           = 40, // <add> ::= identfier '=' identfier '+' identfier End
        RULE_SUB_IDENTFIER_EQ_IDENTFIER_MINUS_INT_END                                                = 41, // <sub> ::= identfier '=' identfier '-' int End
        RULE_SUB_IDENTFIER_MINUSEQ_INT_END                                                           = 42, // <sub> ::= identfier '-=' int End
        RULE_SUB_IDENTFIER_EQ_IDENTFIER_MINUS_IDENTFIER_END                                          = 43, // <sub> ::= identfier '=' identfier '-' identfier End
        RULE_DIV_IDENTFIER_EQ_IDENTFIER_DIV_INT_END                                                  = 44, // <div> ::= identfier '=' identfier '/' int End
        RULE_DIV_IDENTFIER_DIVEQ_INT_END                                                             = 45, // <div> ::= identfier '/=' int End
        RULE_DIV_IDENTFIER_EQ_ID_DIV_ID_END                                                          = 46, // <div> ::= identfier '=' ID '/' ID End
        RULE_MUL_IDENTFIER_EQ_IDENTFIER_TIMES_INT_END                                                = 47, // <mul> ::= identfier '=' identfier '*' int End
        RULE_MUL_IDENTFIER_TIMESEQ_INT_END                                                           = 48, // <mul> ::= identfier '*=' int End
        RULE_MUL_IDENTFIER_EQ_IDENTFIER_TIMES_IDENTFIER_END                                          = 49  // <mul> ::= identfier '=' identfier '*' identfier End
    };

    public class MyParser
    {
        private LALRParser parser;

        public MyParser(string filename)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMFALSE :
                //'!false'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESEQ :
                //'*='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIVEQ :
                //'/='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSEQ :
                //'+='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSEQ :
                //'-='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE :
                //case
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSIF :
                //elsif
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //End
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ENDELSIF :
                //endelsif
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ENDFOR :
                //endfor
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ENDIF :
                //endif
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ENDSWITCH :
                //endswitch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ENDWHILE :
                //endwhile
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT :
                //Float
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //ID
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IDENTFIER :
                //identfier
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTEGER :
                //Integer
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //Start
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TRUE :
                //true
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ADD :
                //<add>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGMENT :
                //<assigment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONTANT :
                //<contant>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DECLARATION :
                //<declaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV2 :
                //<div>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORAS :
                //<foras>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORINCDEC :
                //<forincdec>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MUL :
                //<mul>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATMENTLIST :
                //<statmentlist>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SUB :
                //<sub>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_START_LBRACE_RBRACE_END :
                //<program> ::= Start '{' <contant> '}' End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT :
                //<contant> ::= <assigment> <statmentlist>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT2 :
                //<contant> ::= <declaration> <statmentlist>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE_ENDFOR :
                //<contant> ::= for '(' <foras> ';' <exp> ';' <forincdec> ')' '{' <statmentlist> '}' endfor
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT_WHILE_LPAREN_RPAREN_LBRACE_RBRACE_ENDWHILE :
                //<contant> ::= while '(' <exp> ')' '{' <statmentlist> '}' endwhile
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT_SWITCH_LPAREN_IDENTFIER_RPAREN_LBRACE_CASE_INTEGER_COLON_RBRACE_ENDSWITCH :
                //<contant> ::= switch '(' identfier ')' '{' case Integer ':' <statmentlist> '}' endswitch
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ENDIF :
                //<contant> ::= if '(' <exp> ')' '{' <statmentlist> '}' endif
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ENDIF_ELSIF_LPAREN_RPAREN_LBRACE_RBRACE_ENDELSIF :
                //<contant> ::= if '(' <exp> ')' '{' <statmentlist> '}' endif elsif '(' <exp> ')' '{' <statmentlist> '}' endelsif
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGMENT_IDENTFIER_END :
                //<assigment> ::= identfier End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECLARATION_IDENTFIER_EQ_INT_END :
                //<declaration> ::= identfier '=' int End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECLARATION_IDENTFIER_EQ_FLOAT_END :
                //<declaration> ::= identfier '=' Float End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORAS_IDENTFIER_EQ_INT :
                //<foras> ::= identfier '=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORAS_IDENTFIER :
                //<foras> ::= identfier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_IDENTFIER_PLUSPLUS :
                //<forincdec> ::= identfier '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_IDENTFIER_MINUSMINUS :
                //<forincdec> ::= identfier '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_PLUSPLUS_IDENTFIER :
                //<forincdec> ::= '++' identfier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_MINUSMINUS_IDENTFIER :
                //<forincdec> ::= '--' identfier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_IDENTFIER_PLUSEQ_INT :
                //<forincdec> ::= identfier '+=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_IDENTFIER_MINUSEQ_INT :
                //<forincdec> ::= identfier '-=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_IDENTFIER_TIMESEQ_INT :
                //<forincdec> ::= identfier '*=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_IDENTFIER_DIVEQ_INT :
                //<forincdec> ::= identfier '/=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_IDENTFIER_EQ_IDENTFIER_PLUS_INT :
                //<forincdec> ::= identfier '=' identfier '+' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_IDENTFIER_EQ_IDENTFIER_MINUS_INT :
                //<forincdec> ::= identfier '=' identfier '-' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_IDENTFIER_EQ_IDENTFIER_TIMES_INT :
                //<forincdec> ::= identfier '=' identfier '*' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_IDENTFIER_EQ_IDENTFIER_DIV_INT :
                //<forincdec> ::= identfier '=' identfier '/' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_IDENTFIER_LTEQ_INT :
                //<exp> ::= identfier '<=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_IDENTFIER_GTEQ_INT :
                //<exp> ::= identfier '>=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_IDENTFIER_GT_INT :
                //<exp> ::= identfier '>' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_IDENTFIER_LT_INT :
                //<exp> ::= identfier '<' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_IDENTFIER_EXCLAMEQ_INT :
                //<exp> ::= identfier '!=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_TRUE :
                //<exp> ::= true
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_EXCLAMFALSE :
                //<exp> ::= '!false'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENTLIST :
                //<statmentlist> ::= <contant> <statmentlist>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENTLIST2 :
                //<statmentlist> ::= <add> <statmentlist>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENTLIST3 :
                //<statmentlist> ::= <sub> <statmentlist>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENTLIST4 :
                //<statmentlist> ::= <div> <statmentlist>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENTLIST5 :
                //<statmentlist> ::= <mul> <statmentlist>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENTLIST6 :
                //<statmentlist> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADD_IDENTFIER_EQ_IDENTFIER_PLUS_INT_END :
                //<add> ::= identfier '=' identfier '+' int End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADD_IDENTFIER_PLUSEQ_INT_END :
                //<add> ::= identfier '+=' int End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADD_IDENTFIER_EQ_IDENTFIER_PLUS_IDENTFIER_END :
                //<add> ::= identfier '=' identfier '+' identfier End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SUB_IDENTFIER_EQ_IDENTFIER_MINUS_INT_END :
                //<sub> ::= identfier '=' identfier '-' int End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SUB_IDENTFIER_MINUSEQ_INT_END :
                //<sub> ::= identfier '-=' int End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SUB_IDENTFIER_EQ_IDENTFIER_MINUS_IDENTFIER_END :
                //<sub> ::= identfier '=' identfier '-' identfier End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIV_IDENTFIER_EQ_IDENTFIER_DIV_INT_END :
                //<div> ::= identfier '=' identfier '/' int End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIV_IDENTFIER_DIVEQ_INT_END :
                //<div> ::= identfier '/=' int End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIV_IDENTFIER_EQ_ID_DIV_ID_END :
                //<div> ::= identfier '=' ID '/' ID End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MUL_IDENTFIER_EQ_IDENTFIER_TIMES_INT_END :
                //<mul> ::= identfier '=' identfier '*' int End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MUL_IDENTFIER_TIMESEQ_INT_END :
                //<mul> ::= identfier '*=' int End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MUL_IDENTFIER_EQ_IDENTFIER_TIMES_IDENTFIER_END :
                //<mul> ::= identfier '=' identfier '*' identfier End
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'";
            //todo: Report message to UI?
        }

    }
}
