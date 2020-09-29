using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

namespace TextSearcherMethod
{
    class Items
    {
        public string file { get; set; }
        public string create_time { get; set; }
    }

    class Program
    {
        static int Main(string[] args)
        {
            // 引数チェック
            //引数の数が正しく入力されていることの確認 
            if (args.Length != 1 && args.Length != 0)
            {
                Console.Write("引数はひとつにしてください\n");
                return -1;
            }
            //引数が入力されていることの確認
            else if (args.Length == 0)
            {
                Console.Write("引数に対象のフォルダパスを入力してください\n");
                return -1;
            }

            //fainally用
            StreamReader reader = null;

            try
            {
                // SearchOption.AllDirectoriesを利用する方法
                string[] names = Directory.GetFiles(args[0], "*.txt", SearchOption.AllDirectories);
                if (names[0] != null)
                {
                    foreach (string subnames in names)
                    {
                        var date = new FileInfo(subnames);

                        // JSON化前にリスト化する
                        List<Items> texts = new List<Items>
                        {
                            new Items{ file = subnames , create_time = date.CreationTime.ToString()}
                        };

                        //JSON化
                        string json = JsonSerializer.Serialize(texts);

                        //コンソール出力
                        Console.WriteLine(json);
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                // エラー処理1
                // ディレクトリが存在しなかったり、アクセス権限がない場合にここが実行される。
                Console.Write("\nディレクトリが存在しない、もしくはアクセス権がありません");
                return -1;
            }
            catch (Exception)
            {
                // エラー処理2
                // ファイルが存在しなかったり、アクセス権限がない場合にここが実行される。
                Console.Write("\n該当のファイルが存在しない、もしくはアクセス権がありません");
                return -1;
            }
            finally
            {
                // 例外の有無にかかわらず終了する
                if (reader != null)
                    reader.Close();
            }

            return 0;

        }
    }

}
