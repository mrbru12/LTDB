using System.Collections.Generic;

namespace LTDB
{
    public static class TxtSet
    {
        // [^-^]======================|VARIÁVEIS E CONSTANTES PADRÃO|======================[^-^]

        // private static readonly char[] prohibitedChars = new char[] { '~', ':', '<', '>', '-', '#' };
        private static readonly char[] prohibitedChars = new char[] { }; // DEIXA VAZIO ENQUANTO EU NÃO PENSO EM ALGO MELHOR!

        // [^-^]======================|CHECKERS DE CHARS PROIBÍDOS|======================[^-^]
              
        private static bool IsBadName(string varName)
        {
            foreach (char letter in varName)
            {
                foreach (char pLetter in prohibitedChars)
                {
                    if (letter == pLetter)
                        return true;
                }
            }

            return false;
        }

        private static bool IsBadContent(string varContent)
        {
            foreach (char letter in varContent)
            {
                foreach (char pLetter in prohibitedChars)
                {
                    if (letter == pLetter)
                        return true;
                }
            }

            return false;
        }

        // [^-^]======================|MÉTODO: ADICIONAR UMA VARIÁVEL|======================[^-^]

        public static void SetVar(ref List<string> txtLines, string varName, string varContent)
        {
            if (IsBadName(varName) || IsBadContent(varContent))
                return;

            txtLines.Add('~' + varName + ':' + varContent);
        }

        // [^-^]======================|MÉTODO: ADICIONAR UM ARRAY|======================[^-^]

        public static void SetArray(ref List<string> txtLines, string arrayName, string[] arrayLines)
        {
            if (IsBadName(arrayName))
                return;
            foreach (string line in arrayLines) // ATENÇÃO: Talvez isso seja meio ineficiente ;-; 
            {
                if (IsBadContent(line))
                    return;
            }

            txtLines.Add('<' + arrayName);

            foreach (string line in arrayLines)
            {
                txtLines.Add('-' + line);
            }

            txtLines.Add(">");
        }

        // [^-^]======================|MÉTODO: ADICIONAR UM COMENTÁRIO|======================[^-^]

        public static void SetComment(ref List<string> txtLines, string content) 
        {
            if (IsBadContent(content))
                return;

            txtLines.Add('#' + content);
        }

        // [^-^]======================|vvv ° FUTURO LOKASSO ° vvv|======================[^-^]
    }
}
