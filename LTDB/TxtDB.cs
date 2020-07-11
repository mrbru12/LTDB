using System.IO;
using System.Collections.Generic;
using System;
// [^-^]======================|ERROS E BUGS ATUAIS|======================[^-^]
// ---> SE TIVER UMA LINHA EM BRANCO ("") NO ARQUIVO, QUANDO OS MÉTODOS TENTAM FAZER INDEXAÇÃO DOS... 
// CHARS DELA, EX: linha[0]; DA ERRO PORQUE ELA É NULA E NÃO TEM NENHUM ÍNDICE.

namespace LTDB  // ° { LokassoTxtDataBase! :D } °
{
    public class TxtDB
    {
        // [^-^]======================|CONSTRUTOR|======================[^-^]

        public TxtDB(string txtPath)
        {
            TxtPath = txtPath;

            if (!File.Exists(TxtPath))
                File.Create(TxtPath).Close();
            
            Lines = new List<string>();
            foreach (string line in File.ReadAllLines(TxtPath))
            {
                if (line != "")
                    Lines.Add(line);
                else
                    Console.WriteLine("linha vazia");  // TA AQUI POR ENQUANTO PRA TESTA O ERRO QUE TA DANDO!!!
            }
        }

        // [^-^]======================|VARIÁVEIS, CONSTANTES E PROPRIEDADES PADRÃO|======================[^-^]

        public string TxtPath { get; }
        // private ArrayList Lines { get; } REFERÊNCIA NÃO FUNCIONA COM PROPRIEDADE ;-;
        private List<string> Lines;

        // [^-^]======================|MÉTODOS STATIC E PRIVADOS GENÉRICOS|======================[^-^]

        internal static string[] ListToStrArray(List<string> list)
        {
            string[] strArray = new string[list.Count];

            object[] tempStrArray = list.ToArray();
            tempStrArray.CopyTo(strArray, 0);

            return strArray;
        }

        private static void LogToTxt(string path, string[] lines)
        {
            File.WriteAllText(path, string.Empty);
            File.WriteAllLines(path, lines);
        }

        // [^-^]======================|MÉTODOS LTBD DE GET SIMPLIFICADOS|======================[^-^]

        public string GetVar(string varName)
        {
            return TxtGet.GetVar(Lines, varName);
        }

        public string[] GetArray(string varName)
        {
            return TxtGet.GetArray(Lines, varName);
        }

        public string[] GetVarNames() // <- O PROBLEMA TA AQUI! 0_0
        {
            return TxtGet.GetVarNames(Lines);
        }

        public string[] GetArrayNames() // <- O PROBLEMA TA AQUI! 0_0
        {
            return TxtGet.GetArrayNames(Lines);
        }

        // [^-^]======================|MÉTODOS LTBD DE SET SIMPLIFICADOS|======================[^-^]

        public void Clear()
        {
            // File.WriteAllText(TxtPath, string.Empty);
            Lines = new List<string>();
        }

        public void SetVar(string varName, string varContent)
        {
            TxtSet.SetVar(ref Lines, varName, varContent);

            LogToTxt(TxtPath, ListToStrArray(Lines));
        }

        public void SetArray(string varName, string[] varLines)
        {
            TxtSet.SetArray(ref Lines, varName, varLines);

            LogToTxt(TxtPath, ListToStrArray(Lines));
        }

        public void SetComment(string content)
        {
            TxtSet.SetComment(ref Lines, content);

            LogToTxt(TxtPath, ListToStrArray(Lines));
        }

        // [^-^]======================|vvv ° FUTURO LOKASSO ° vvv|======================[^-^]
    }
}
