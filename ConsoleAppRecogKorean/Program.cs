using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleAppRecogKorean
{
    class Program
    {
        static void Main(string[] args)
        {

            string str = "쌿쩬 찙 빫 힣sp 고메니오당수땋 ㅀ ㄱㄴㄷㄺㄹㄴㄾㄸㄲㅃㅉㅌㄿㅎ 日本なのに区分でしょうか？ Это японский, можешь мне сказать? 这是日语，你能告诉我吗？ 這是日語，你能告訴我嗎？";
            int unicode = 0;
            int tmpfirst = 0;
            int tmpend = 0;
            string first = "";
            string mid = "";
            string end = "";
            StringBuilder strBuilder = new StringBuilder();
            foreach (char singleChar in str)
            {
                //bool IsKorean = Regex.IsMatch(singleChar.ToString(), "[가-힣]");
                //int asciicode = singleChar;
                //byte[] chrBytes = BitConverter.GetBytes(asciicode);

                //int count = 0;
                //Console.Write(string.Format("한글인가? {0}: {1} / ", singleChar, IsKorean));
                //foreach (byte b in chrBytes)
                //{
                //    Console.Write(string.Format("0x{0:X} ", b));
                //    count++;
                //    if (chrBytes.Length == count)
                //        Console.WriteLine();
                //}

                if (Regex.IsMatch(singleChar.ToString(), "[가-힣]"))
                {
                    unicode = Convert.ToInt32(singleChar);
                    tmpfirst = (unicode - 44032) / 588;
                    tmpend = (unicode - 44032) % 28;
                    first = Convert.ToChar(12593 + tmpfirst + (tmpfirst < 2 ? 0 : tmpfirst < 3 ? 1 : tmpfirst < 6 ? 3 : tmpfirst < 9 ? 10 : 11)).ToString();
                    mid = Convert.ToChar(((unicode - 44032) % 588) / 28 + 12623).ToString();
                    end = tmpend == 0 ? "" : Convert.ToChar((12592 + tmpend + (tmpend < 8 ? 0 : tmpend < 18 ? 1 : tmpend < 23 ? 2 : 3))).ToString();
                    strBuilder.Append(first + mid + end);
                }
                else
                {
                    strBuilder.Append(singleChar);

                    bool IsKor = Regex.IsMatch(singleChar.ToString(), "[ㄱ-ㅎ]");
                    if (IsKor)
                    {
                        Console.WriteLine(string.Format("분해된 한글??? {0}: {1}", singleChar, true));
                        Console.WriteLine();
                    }
                }
            }

            Console.WriteLine(str + "\n\n" + strBuilder.ToString());

            //String tmpStr = "/u0000한글";
            //byte[] bytes = Encoding.Default.GetBytes(tmpStr);

            //foreach (byte b in bytes)
            //{
            //    Console.Write(String.Format("0x{0:X} ", b));
            //}

            //Console.WriteLine();

            //foreach (byte aByte in Encoding.GetEncoding("EUC-KR").GetBytes(tmpStr))
            //{
            //    Console.Write("0x" + aByte.ToString("x") + " ");
            //}
            //Console.WriteLine();
        }
    }
}
