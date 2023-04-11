
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;


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
        SYMBOL_AUTO         = 26, // auto
        SYMBOL_CASE         = 27, // case
        SYMBOL_ELSIF        = 28, // elsif
        SYMBOL_END          = 29, // End
        SYMBOL_FLOAT        = 30, // Float
        SYMBOL_FOR          = 31, // for
        SYMBOL_ID           = 32, // ID
        SYMBOL_IF           = 33, // if
        SYMBOL_INTEGER      = 34, // Integer
        SYMBOL_START        = 35, // Start
        SYMBOL_SWITCH       = 36, // switch
        SYMBOL_TRUE         = 37, // true
        SYMBOL_WHILE        = 38, // while
        SYMBOL_ADD          = 39, // <add>
        SYMBOL_ASSIGMENT    = 40, // <assigment>
        SYMBOL_CONTANT      = 41, // <contant>
        SYMBOL_DECLARATION  = 42, // <declaration>
        SYMBOL_DIV2         = 43, // <div>
        SYMBOL_EXP          = 44, // <exp>
        SYMBOL_FORAS        = 45, // <foras>
        SYMBOL_FORINCDEC    = 46, // <forincdec>
        SYMBOL_MUL          = 47, // <mul>
        SYMBOL_PROGRAM      = 48, // <program>
        SYMBOL_STATMENTLIST = 49, // <statmentlist>
        SYMBOL_SUB          = 50  // <sub>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_START_LBRACE_RBRACE_END                                          =  0, // <program> ::= Start '{' <contant> '}' End
        RULE_CONTANT                                                                  =  1, // <contant> ::= <assigment> <statmentlist>
        RULE_CONTANT2                                                                 =  2, // <contant> ::= <declaration> <statmentlist>
        RULE_CONTANT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE                        =  3, // <contant> ::= for '(' <foras> ';' <exp> ';' <forincdec> ')' '{' <statmentlist> '}'
        RULE_CONTANT_WHILE_LPAREN_RPAREN_LBRACE_RBRACE                                =  4, // <contant> ::= while '(' <exp> ')' '{' <statmentlist> '}'
        RULE_CONTANT_SWITCH_LPAREN_ID_RPAREN_LBRACE_CASE_INTEGER_COLON_RBRACE         =  5, // <contant> ::= switch '(' ID ')' '{' case Integer ':' <statmentlist> '}'
        RULE_CONTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE                                   =  6, // <contant> ::= if '(' <exp> ')' '{' <statmentlist> '}'
        RULE_CONTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ELSIF_LPAREN_RPAREN_LBRACE_RBRACE =  7, // <contant> ::= if '(' <exp> ')' '{' <statmentlist> '}' elsif '(' <exp> ')' '{' <statmentlist> '}'
        RULE_ASSIGMENT_ID_SEMI                                                        =  8, // <assigment> ::= ID ';'
        RULE_DECLARATION_ID_EQ_INTEGER_SEMI                                           =  9, // <declaration> ::= ID '=' Integer ';'
        RULE_DECLARATION_ID_EQ_FLOAT_SEMI                                             = 10, // <declaration> ::= ID '=' Float ';'
        RULE_FORAS_AUTO_ID_EQ_INTEGER                                                 = 11, // <foras> ::= auto ID '=' Integer
        RULE_FORAS_ID_EQ_INTEGER                                                      = 12, // <foras> ::= ID '=' Integer
        RULE_FORAS_ID                                                                 = 13, // <foras> ::= ID
        RULE_FORINCDEC_ID_PLUSPLUS                                                    = 14, // <forincdec> ::= ID '++'
        RULE_FORINCDEC_ID_MINUSMINUS                                                  = 15, // <forincdec> ::= ID '--'
        RULE_FORINCDEC_PLUSPLUS_ID                                                    = 16, // <forincdec> ::= '++' ID
        RULE_FORINCDEC_MINUSMINUS_ID                                                  = 17, // <forincdec> ::= '--' ID
        RULE_FORINCDEC_ID_PLUSEQ_INTEGER                                              = 18, // <forincdec> ::= ID '+=' Integer
        RULE_FORINCDEC_ID_MINUSEQ_INTEGER                                             = 19, // <forincdec> ::= ID '-=' Integer
        RULE_FORINCDEC_ID_TIMESEQ_INTEGER                                             = 20, // <forincdec> ::= ID '*=' Integer
        RULE_FORINCDEC_ID_DIVEQ_INTEGER                                               = 21, // <forincdec> ::= ID '/=' Integer
        RULE_FORINCDEC_ID_EQ_ID_PLUS_INTEGER                                          = 22, // <forincdec> ::= ID '=' ID '+' Integer
        RULE_FORINCDEC_ID_EQ_ID_MINUS_INTEGER                                         = 23, // <forincdec> ::= ID '=' ID '-' Integer
        RULE_FORINCDEC_ID_EQ_ID_TIMES_INTEGER                                         = 24, // <forincdec> ::= ID '=' ID '*' Integer
        RULE_FORINCDEC_ID_EQ_ID_DIV_INTEGER                                           = 25, // <forincdec> ::= ID '=' ID '/' Integer
        RULE_EXP_ID_LTEQ_INTEGER                                                      = 26, // <exp> ::= ID '<=' Integer
        RULE_EXP_ID_GTEQ_INTEGER                                                      = 27, // <exp> ::= ID '>=' Integer
        RULE_EXP_ID_GT_INTEGER                                                        = 28, // <exp> ::= ID '>' Integer
        RULE_EXP_ID_LT_INTEGER                                                        = 29, // <exp> ::= ID '<' Integer
        RULE_EXP_ID_EXCLAMEQ_INTEGER                                                  = 30, // <exp> ::= ID '!=' Integer
        RULE_EXP_TRUE                                                                 = 31, // <exp> ::= true
        RULE_EXP_EXCLAMFALSE                                                          = 32, // <exp> ::= '!false'
        RULE_STATMENTLIST                                                             = 33, // <statmentlist> ::= <contant> <statmentlist>
        RULE_STATMENTLIST2                                                            = 34, // <statmentlist> ::= <add> <statmentlist>
        RULE_STATMENTLIST3                                                            = 35, // <statmentlist> ::= <sub> <statmentlist>
        RULE_STATMENTLIST4                                                            = 36, // <statmentlist> ::= <div> <statmentlist>
        RULE_STATMENTLIST5                                                            = 37, // <statmentlist> ::= <mul> <statmentlist>
        RULE_STATMENTLIST6                                                            = 38, // <statmentlist> ::= 
        RULE_ADD_ID_EQ_ID_PLUS_INTEGER                                                = 39, // <add> ::= ID '=' ID '+' Integer
        RULE_ADD_ID_PLUSEQ_INTEGER                                                    = 40, // <add> ::= ID '+=' Integer
        RULE_ADD_ID_EQ_ID_PLUS_ID                                                     = 41, // <add> ::= ID '=' ID '+' ID
        RULE_SUB_ID_EQ_ID_MINUS_INTEGER                                               = 42, // <sub> ::= ID '=' ID '-' Integer
        RULE_SUB_ID_MINUSEQ_INTEGER                                                   = 43, // <sub> ::= ID '-=' Integer
        RULE_SUB_ID_EQ_ID_MINUS_ID                                                    = 44, // <sub> ::= ID '=' ID '-' ID
        RULE_DIV_ID_EQ_ID_DIV_INTEGER                                                 = 45, // <div> ::= ID '=' ID '/' Integer
        RULE_DIV_ID_DIVEQ_INTEGER                                                     = 46, // <div> ::= ID '/=' Integer
        RULE_DIV_ID_EQ_ID_DIV_ID                                                      = 47, // <div> ::= ID '=' ID '/' ID
        RULE_MUL_ID_EQ_ID_TIMES_INTEGER                                               = 48, // <mul> ::= ID '=' ID '*' Integer
        RULE_MUL_ID_TIMESEQ_INTEGER                                                   = 49, // <mul> ::= ID '*=' Integer
        RULE_MUL_ID_EQ_ID_TIMES_ID                                                    = 50  // <mul> ::= ID '=' ID '*' ID
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lst;
        public MyParser(string filename,ListBox lst)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            this.lst = lst;
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

                case (int)SymbolConstants.SYMBOL_AUTO :
                //auto
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

                case (int)SymbolConstants.SYMBOL_IF :
                //if
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

                case (int)RuleConstants.RULE_CONTANT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE :
                //<contant> ::= for '(' <foras> ';' <exp> ';' <forincdec> ')' '{' <statmentlist> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT_WHILE_LPAREN_RPAREN_LBRACE_RBRACE :
                //<contant> ::= while '(' <exp> ')' '{' <statmentlist> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT_SWITCH_LPAREN_ID_RPAREN_LBRACE_CASE_INTEGER_COLON_RBRACE :
                //<contant> ::= switch '(' ID ')' '{' case Integer ':' <statmentlist> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE :
                //<contant> ::= if '(' <exp> ')' '{' <statmentlist> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ELSIF_LPAREN_RPAREN_LBRACE_RBRACE :
                //<contant> ::= if '(' <exp> ')' '{' <statmentlist> '}' elsif '(' <exp> ')' '{' <statmentlist> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGMENT_ID_SEMI :
                //<assigment> ::= ID ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECLARATION_ID_EQ_INTEGER_SEMI :
                //<declaration> ::= ID '=' Integer ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECLARATION_ID_EQ_FLOAT_SEMI :
                //<declaration> ::= ID '=' Float ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORAS_AUTO_ID_EQ_INTEGER :
                //<foras> ::= auto ID '=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORAS_ID_EQ_INTEGER :
                //<foras> ::= ID '=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORAS_ID :
                //<foras> ::= ID
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_PLUSPLUS :
                //<forincdec> ::= ID '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_MINUSMINUS :
                //<forincdec> ::= ID '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_PLUSPLUS_ID :
                //<forincdec> ::= '++' ID
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_MINUSMINUS_ID :
                //<forincdec> ::= '--' ID
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_PLUSEQ_INTEGER :
                //<forincdec> ::= ID '+=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_MINUSEQ_INTEGER :
                //<forincdec> ::= ID '-=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_TIMESEQ_INTEGER :
                //<forincdec> ::= ID '*=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_DIVEQ_INTEGER :
                //<forincdec> ::= ID '/=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_EQ_ID_PLUS_INTEGER :
                //<forincdec> ::= ID '=' ID '+' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_EQ_ID_MINUS_INTEGER :
                //<forincdec> ::= ID '=' ID '-' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_EQ_ID_TIMES_INTEGER :
                //<forincdec> ::= ID '=' ID '*' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_EQ_ID_DIV_INTEGER :
                //<forincdec> ::= ID '=' ID '/' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_ID_LTEQ_INTEGER :
                //<exp> ::= ID '<=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_ID_GTEQ_INTEGER :
                //<exp> ::= ID '>=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_ID_GT_INTEGER :
                //<exp> ::= ID '>' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_ID_LT_INTEGER :
                //<exp> ::= ID '<' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_ID_EXCLAMEQ_INTEGER :
                //<exp> ::= ID '!=' Integer
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

                case (int)RuleConstants.RULE_ADD_ID_EQ_ID_PLUS_INTEGER :
                //<add> ::= ID '=' ID '+' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADD_ID_PLUSEQ_INTEGER :
                //<add> ::= ID '+=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADD_ID_EQ_ID_PLUS_ID :
                //<add> ::= ID '=' ID '+' ID
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SUB_ID_EQ_ID_MINUS_INTEGER :
                //<sub> ::= ID '=' ID '-' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SUB_ID_MINUSEQ_INTEGER :
                //<sub> ::= ID '-=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SUB_ID_EQ_ID_MINUS_ID :
                //<sub> ::= ID '=' ID '-' ID
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIV_ID_EQ_ID_DIV_INTEGER :
                //<div> ::= ID '=' ID '/' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIV_ID_DIVEQ_INTEGER :
                //<div> ::= ID '/=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIV_ID_EQ_ID_DIV_ID :
                //<div> ::= ID '=' ID '/' ID
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MUL_ID_EQ_ID_TIMES_INTEGER :
                //<mul> ::= ID '=' ID '*' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MUL_ID_TIMESEQ_INTEGER :
                //<mul> ::= ID '*=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MUL_ID_EQ_ID_TIMES_ID :
                //<mul> ::= ID '=' ID '*' ID
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
            string message = "Parse error caused by token:  "+args.UnexpectedToken.ToString()+" In Line: "+args.UnexpectedToken.Location.LineNr+" ExpectedToken: "+args.UnexpectedToken.ToString();
            lst.Items.Add(message);
            //todo: Report message to UI?
        }

    }
}
