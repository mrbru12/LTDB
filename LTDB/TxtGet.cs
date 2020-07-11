using System.Collections.Generic;

namespace LTDB
{
    public static class TxtGet 
    {
        // [^-^]======================|CHECKERS DE TIPOS DE VERIÁVEIS|======================[^-^]

        public static bool IsVar(string line)
        {
            return (line[0] == '~') ? true : false;
        }

        
        public static bool IsArray(string line)
        {
            return (line[0] == '<' || line[0] == '-' || line[0] == '>') ? true : false;
        }
        

        public static bool IsComment(string line)
        {
            return (line[0] == '#') ? true : false;
        }

        public static bool IsFromLTDB(string line)
        {
            if (IsVar(line))
                return true;
            
            if (IsArray(line))
                return true;
            
            if (IsComment(line))
                return true;

            return false;
        }

        // [^-^]======================|MÉTODO: PEGAR UMA VARIÁVEL|======================[^-^]

        public static string GetVar(List<string> txtLines, string varName)
        {
            int currentLine = 0;

            foreach (string line in txtLines)
            {
                switch (line[0])
                {
                    case '~':
                        int charI = 1;
                        string name = "";

                        while (line[charI] != ':')
                        {
                            name += line[charI];
                            charI++;
                        }

                        if (name == varName)
                            return line.Remove(0, charI + 1);

                        break;

                    default:
                        continue;
                }

                currentLine++;
            }

            return null;
        }

        /*
        public static string GetVar(string path, string varName)
        {
            List<string> lines = new List<string>();

            return GetVar(lines, varName);
        }
        */

        // [^-^]======================|MÉTODO: PEGAR UM ARRAY|======================[^-^]

        public static string[] GetArray(List<string> txtLines, string arrayName)
        {
            string[] txtStrLines = TxtDB.ListToStrArray(txtLines);
            int currentLine = 0;

            foreach (string line in txtStrLines)
            {
                switch (line[0])
                {
                    case '<':
                        string name = "";

                        for (int i = 1; i < line.Length; i++)
                            name += line[i];

                        if (name == arrayName)
                        {
                            string[] linesValues;
                            int lineCount = 0;
                            
                            for (int i = currentLine; i < txtStrLines.Length; i++)
                            {
                                if (txtStrLines[i][0] != '>')
                                    lineCount++;
                                else
                                    break;
                            }

                            linesValues = new string[lineCount];
                            int insideCount = 0;

                            for (int i = currentLine + 1; i < currentLine + lineCount; i++)
                            {
                                linesValues[insideCount] += txtStrLines[i].Remove(0, 1);

                                insideCount++;
                            }

                            return linesValues;
                        }

                        break;

                    default:
                        break;
                }

                currentLine++;
            }

            return null;
        }

        // [^-^]======================|MÉTODOS PARA LISTAR TODOS OS NOMES|======================[^-^]

        public static string[] GetVarNames(List<string> txtLines)
        {
            string[] txtStrLines = TxtDB.ListToStrArray(txtLines);
            string[] varNames;
            int varCount = 0;
            
            foreach (string line in txtStrLines)
            {
                if (line[0] == '~')
                    varCount++;
            }
            varNames = new string[varCount];

            int count = 0;
            foreach (string line in txtStrLines)
            {
                string varName = "";
                int lineX = 1;
                
                if (line[0] == '~')
                {
                    while (line[lineX] != ':')
                    {
                        varName += line[lineX];
                        
                        lineX++;
                    }
                    varNames[count] = varName;

                    count++;
                }
            }
            
            return varNames;
        }

        public static string[] GetArrayNames(List<string> txtLines)
        {
            string[] txtStrLines = TxtDB.ListToStrArray(txtLines);
            string[] arrayNames;
            int arrayCount = 0;

            foreach (string line in txtStrLines)
            {
                if (line[0] == '<')
                    arrayCount++;
            }
            arrayNames = new string[arrayCount];

            int count = 0;
            foreach (string line in txtStrLines)
            {
                if (line[0] == '<')
                {
                    arrayNames[count] = line.Remove(0, 1);

                    count++;
                }
            }

            return arrayNames;
        }

        // [^-^]======================|vvv ° FUTURO LOKASSO ° vvv|======================[^-^]
    }
}
