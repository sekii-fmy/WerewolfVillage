using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WerewolfVillage
{
    class TagToUtterance
    {
        public GameData convertUtteranceToTag(GameData gameData)
        {
            string[] values = gameData.Utterance.Split(' ');
            foreach (string value in values)
            {
                string utter = value;
                if (Regex.IsMatch(utter, "私の役職は.+です。"))
                {
                    utter = utter.Substring(5);
                    utter = utter.Remove(utter.Length - 3);
                    gameData.Tag += "CO ";
                    gameData.Guess += Complement(gameData.Name) + ":" + utter + "CO ";
                }
                else if (Regex.IsMatch(utter, "私は.+です。"))
                {
                    utter = utter.Substring(2);
                    utter = utter.Remove(utter.Length - 3);
                    gameData.Tag += "CO ";
                    gameData.Guess += Complement(gameData.Name) + ":" + utter + "CO ";
                }
                else if (Regex.IsMatch(utter, "私の役職は.+ではありません。"))
                {
                    utter = utter.Substring(5);
                    utter = utter.Remove(utter.Length - 8);
                    gameData.Tag += "CO ";
                    gameData.Guess += Complement(gameData.Name) + ":非" + utter + "CO ";
                }
                else if (Regex.IsMatch(utter, "私は.+ではありません。"))
                {
                    utter = utter.Substring(2);
                    utter = utter.Remove(utter.Length - 8);
                    gameData.Tag += "CO ";
                    gameData.Guess += Complement(gameData.Name) + ":非" + utter + "CO ";
                }
                else if (Regex.IsMatch(utter, ".+の役職は.+だと思います。"))
                {
                    if (utter.Contains("村人寄り"))
                    {
                        utter = utter.Remove(utter.Length - 15);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":村人寄り ";
                    }
                    else if (utter.Contains("村人"))
                    {
                        utter = utter.Remove(utter.Length - 13);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":村人 ";
                    }
                    else if (utter.Contains("占い師寄り"))
                    {
                        utter = utter.Remove(utter.Length - 16);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":占い師寄り ";
                    }
                    else if (utter.Contains("占い師"))
                    {
                        utter = utter.Remove(utter.Length - 14);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":占い師 ";
                    }
                    else if (utter.Contains("霊能者寄り"))
                    {
                        utter = utter.Remove(utter.Length - 16);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":霊能者寄り ";
                    }
                    else if (utter.Contains("霊能者"))
                    {
                        utter = utter.Remove(utter.Length - 14);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":霊能者 ";
                    }
                    else if (utter.Contains("狩人寄り"))
                    {
                        utter = utter.Remove(utter.Length - 15);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":狩人寄り ";
                    }
                    else if (utter.Contains("狩人"))
                    {
                        utter = utter.Remove(utter.Length - 13);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":狩人 ";
                    }
                    else if (utter.Contains("狂人寄り"))
                    {
                        utter = utter.Remove(utter.Length - 15);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":狂人寄り ";
                    }
                    else if (utter.Contains("狂人"))
                    {
                        utter = utter.Remove(utter.Length - 13);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":狂人 ";
                    }
                    else if (utter.Contains("人狼寄り"))
                    {
                        utter = utter.Remove(utter.Length - 15);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":人狼寄り ";
                    }
                    else if (utter.Contains("人狼"))
                    {
                        utter = utter.Remove(utter.Length - 13);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":人狼 ";
                    }
                }
                else if (Regex.IsMatch(utter, ".+の役職は.+ではないと思います。"))
                {
                    if (utter.Contains("村人寄り"))
                    {
                        utter = utter.Remove(utter.Length - 15);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":非村人寄り ";
                    }
                    else if (utter.Contains("村人"))
                    {
                        utter = utter.Remove(utter.Length - 13);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":非村人 ";
                    }
                    else if (utter.Contains("占い師寄り"))
                    {
                        utter = utter.Remove(utter.Length - 16);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":非占い師寄り ";
                    }
                    else if (utter.Contains("占い師"))
                    {
                        utter = utter.Remove(utter.Length - 14);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":非占い師 ";
                    }
                    else if (utter.Contains("霊能者寄り"))
                    {
                        utter = utter.Remove(utter.Length - 16);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":非霊能者寄り ";
                    }
                    else if (utter.Contains("霊能者"))
                    {
                        utter = utter.Remove(utter.Length - 14);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":非霊能者 ";
                    }
                    else if (utter.Contains("狩人寄り"))
                    {
                        utter = utter.Remove(utter.Length - 15);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":非狩人寄り ";
                    }
                    else if (utter.Contains("狩人"))
                    {
                        utter = utter.Remove(utter.Length - 13);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":非狩人 ";
                    }
                    else if (utter.Contains("狂人寄り"))
                    {
                        utter = utter.Remove(utter.Length - 15);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":非狂人寄り ";
                    }
                    else if (utter.Contains("狂人"))
                    {
                        utter = utter.Remove(utter.Length - 13);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":非狂人 ";
                    }
                    else if (utter.Contains("人狼寄り"))
                    {
                        utter = utter.Remove(utter.Length - 15);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":非人狼寄り ";
                    }
                    else if (utter.Contains("人狼"))
                    {
                        utter = utter.Remove(utter.Length - 13);
                        gameData.Tag += "考察 ";
                        gameData.Guess += Complement(utter) + ":非人狼 ";
                    }
                }
                else if (Regex.IsMatch(utter, "占い師には.+以外を占ってほしい。"))
                {
                    utter = utter.Substring(5);
                    utter = utter.Remove(utter.Length - 10);
                    gameData.Tag += "占い希望 ";
                    gameData.Guess += "占い希望:非" + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, "占い師には.+を占ってほしい。"))
                {
                    utter = utter.Substring(5);
                    utter = utter.Remove(utter.Length - 8);
                    gameData.Tag += "占い希望 ";
                    gameData.Guess += "占い希望:" + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, ".+の役職が知りたい。"))
                {
                    utter = utter.Remove(utter.Length - 9);
                    gameData.Tag += "占い希望 ";
                    gameData.Guess += "占い希望:" + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, ".+を占う必要はない。"))
                {
                    utter = utter.Remove(utter.Length - 9);
                    gameData.Tag += "占い希望 ";
                    gameData.Guess += "占い希望:非" + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, ".+に投票してほしい。"))
                {
                    utter = utter.Remove(utter.Length - 9);
                    gameData.Tag += "吊り希望 ";
                    gameData.Guess += "吊り希望:" + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, "私は.+を吊ろうと思います。"))
                {
                    utter = utter.Substring(2);
                    utter = utter.Remove(utter.Length - 10);
                    gameData.Tag += "吊り希望";
                    gameData.Guess += "吊り希望:" + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, "私は.+に投票しようと思う。"))
                {
                    utter = utter.Substring(2);
                    utter = utter.Remove(utter.Length - 10);
                    gameData.Tag += "吊り希望 ";
                    gameData.Guess += "吊り希望:" + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, ".+のことは吊りたくない。"))
                {
                    utter = utter.Remove(utter.Length - 11);
                    gameData.Tag += "吊り希望";
                    gameData.Guess += "吊り希望:非" + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, ".+の.+COを確認しました。"))
                {
                    if (utter.Contains("村人"))
                    {
                        if (utter.Contains("非"))
                        {
                            utter = utter.Remove(utter.Length - 14);
                            gameData.Tag += "確認 ";
                            gameData.Guess += Complement(utter) + ":非村人CO確認 ";
                        }
                        else
                        {
                            utter = utter.Remove(utter.Length - 13);
                            gameData.Tag += "確認 ";
                            gameData.Guess += Complement(utter) + ":村人CO確認 ";
                        }
                    }
                    if (utter.Contains("占い師"))
                    {
                        if (utter.Contains("非"))
                        {
                            utter = utter.Remove(utter.Length - 15);
                            gameData.Tag += "確認 ";
                            gameData.Guess += Complement(utter) + ":非占い師CO確認 ";
                        }
                        else
                        {
                            utter = utter.Remove(utter.Length - 14);
                            gameData.Tag += "確認 ";
                            gameData.Guess += Complement(utter) + ":占い師CO確認 ";
                        }
                    }
                    if (utter.Contains("霊能者"))
                    {
                        if (utter.Contains("非"))
                        {
                            utter = utter.Remove(utter.Length - 15);
                            gameData.Tag += "確認 ";
                            gameData.Guess += Complement(utter) + ":非霊能者CO確認 ";
                        }
                        else
                        {
                            utter = utter.Remove(utter.Length - 14);
                            gameData.Tag += "確認 ";
                            gameData.Guess += Complement(utter) + ":霊能者CO確認 ";
                        }
                    }
                    if (utter.Contains("狩人"))
                    {
                        if (utter.Contains("非"))
                        {
                            utter = utter.Remove(utter.Length - 14);
                            gameData.Tag += "確認 ";
                            gameData.Guess += Complement(utter) + ":非狩人CO確認 ";
                        }
                        else
                        {
                            utter = utter.Remove(utter.Length - 13);
                            gameData.Tag += "確認 ";
                            gameData.Guess += Complement(utter) + ":狩人CO確認 ";
                        }
                    }
                    if (utter.Contains("狂人"))
                    {
                        if (utter.Contains("非"))
                        {
                            utter = utter.Remove(utter.Length - 14);
                            gameData.Tag += "確認 ";
                            gameData.Guess += Complement(utter) + ":非狂人CO確認 ";
                        }
                        else
                        {
                            utter = utter.Remove(utter.Length - 13);
                            gameData.Tag += "確認 ";
                            gameData.Guess += Complement(utter) + ":狂人CO確認 ";
                        }
                    }
                    if (utter.Contains("人狼"))
                    {
                        if (utter.Contains("非"))
                        {
                            utter = utter.Remove(utter.Length - 14);
                            gameData.Tag += "確認 ";
                            gameData.Guess += Complement(utter) + ":非人狼CO確認 ";
                        }
                        else
                        {
                            utter = utter.Remove(utter.Length - 13);
                            gameData.Tag += "確認 ";
                            gameData.Guess += Complement(utter) + ":人狼CO確認 ";
                        }
                    }
                }
                else if (Regex.IsMatch(utter, ".+の.+を占って.+の結果を確認しました。"))
                {
                    string str1 = PlayerNameMatch(utter);
                    string str2 = PlayerNameMatch(utter.Substring(str1.Length));
                    if (utter.Contains("人間"))
                    {
                        gameData.Tag += "確認 ";
                        gameData.Guess += "占い確認:" + Complement(str1) + ":"
                            + Complement(str2) + ":人間 ";
                    }
                    else if (utter.Contains("人狼"))
                    {
                        gameData.Tag += "確認 ";
                        gameData.Guess += "占い確認:" + Complement(str1) + ":"
                            + Complement(str2) + ":人間 ";
                    }
                }
                else if (Regex.IsMatch(utter, ".+の.+を霊能して.+の結果を確認しました。"))
                {
                    string str1 = PlayerNameMatch(utter);
                    string str2 = PlayerNameMatch(utter.Substring(str1.Length));
                    if (utter.Contains("人間"))
                    {
                        gameData.Tag += "確認 ";
                        gameData.Guess += "霊能確認:" + Complement(str1) + ":"
                            + Complement(str2) + ":人間 ";
                    }
                    else if (utter.Contains("人狼"))
                    {
                        gameData.Tag += "確認 ";
                        gameData.Guess += "霊能確認:" + Complement(str1) + ":"
                            + Complement(str2) + ":人狼 ";
                    }
                }
                else if (Regex.IsMatch(utter, ".+吊り確認しました。"))
                {
                    utter = utter.Remove(utter.Length - 9);
                    gameData.Tag += "吊り先確認 ";
                    gameData.Guess += "吊り先確認:" + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, "占い先指定.+確認しました。"))
                {
                    utter = utter.Substring(5);
                    utter = utter.Remove(utter.Length - 7);
                    gameData.Tag += "占い先確認 ";
                    gameData.Guess += "占い先確認:" + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, ".+のことは信頼できると思う。"))
                {
                    utter = utter.Remove(utter.Length - 13);
                    gameData.Tag += "信頼度 ";
                    gameData.Guess += Complement(utter) + ":信頼度高 ";
                }
                else if (Regex.IsMatch(utter, "私は.+のことを信頼している。"))
                {
                    utter = utter.Substring(2);
                    utter = utter.Remove(utter.Length - 11);
                    gameData.Tag += "信頼度 ";
                    gameData.Guess += Complement(utter) + ":信頼度高 ";
                }
                else if (Regex.IsMatch(utter, ".+のことは信頼できない。"))
                {
                    utter = utter.Remove(utter.Length - 11);
                    gameData.Tag += "信頼度 ";
                    gameData.Guess += Complement(utter) + ":信頼度低 ";
                }
                else if (Regex.IsMatch(utter, "私は.+のことは信頼していない。"))
                {
                    utter = utter.Substring(2);
                    utter = utter.Remove(utter.Length - 13);
                    gameData.Tag += "信頼度 ";
                    gameData.Guess += Complement(utter) + ":信頼度低 ";
                }
                else if (Regex.IsMatch(utter, ".+と.+はつながっていると思う。"))
                {
                    string str1 = PlayerNameMatch(utter);
                    string str2 = PlayerNameMatch(utter.Substring(str1.Length));
                    gameData.Tag += "ライン ";
                    gameData.Line += "ライン:" + Complement(str1) + Complement(str2) + " ";
                }
                else if (Regex.IsMatch(utter, ".+と.+がラインだと思う。"))
                {
                    string str1 = PlayerNameMatch(utter);
                    string str2 = PlayerNameMatch(utter.Substring(str1.Length));
                    gameData.Tag += "ライン ";
                    gameData.Line += "ライン:" + Complement(str1) + Complement(str2) + " ";
                }
                else if (Regex.IsMatch(utter, ".+と.+は両狼でないと思う。"))
                {
                    string str1 = PlayerNameMatch(utter);
                    string str2 = PlayerNameMatch(utter.Substring(str1.Length));
                    gameData.Tag += "ライン ";
                    gameData.Line += "非ライン:" + Complement(str1) + Complement(str2) + " ";
                }
                else if (Regex.IsMatch(utter, ".+と.+は非ライン。"))
                {
                    string str1 = PlayerNameMatch(utter);
                    string str2 = PlayerNameMatch(utter.Substring(str1.Length));
                    gameData.Tag += "ライン ";
                    gameData.Line += "非ライン:" + Complement(str1) + Complement(str2) + " ";
                }
                else if (Regex.IsMatch(utter, "[/?]$"))
                {

                }
                else if (Regex.IsMatch(utter, ".+の発言に同意する。"))
                {
                    utter = utter.Remove(utter.Length - 9);
                    gameData.Tag += "同調 ";
                    gameData.Questtion += "同調:" + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, ".+に賛成だ。"))
                {
                    utter = utter.Remove(utter.Length - 5);
                    gameData.Tag += "同調 ";
                    gameData.Questtion += "同調:" + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, ".+の発言には反対だ。"))
                {
                    utter = utter.Remove(utter.Length - 9);
                    gameData.Tag += "反発 ";
                    gameData.Questtion += "反発:" + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, ".+には同意できない。"))
                {
                    utter = utter.Remove(utter.Length - 9);
                    gameData.Tag = "反発 ";
                    gameData.Questtion = "反発:" + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, "占った結果、.+は.+だった。"))
                {
                    if (utter.Contains("人間"))
                    {
                        utter = utter.Substring(6);
                        utter = utter.Remove(utter.Length - 6);
                        gameData.Tag += "占い結果 ";
                        gameData.Guess += Complement(utter) + ":人間 ";
                    }
                    if (utter.Contains("人狼"))
                    {
                        utter = utter.Substring(6);
                        utter = utter.Remove(utter.Length - 6);
                        gameData.Tag += "占い結果 ";
                        gameData.Guess += Complement(utter) + ":人狼 ";
                    }
                }
                else if (Regex.IsMatch(utter, "処刑した.+は.+だった。"))
                {
                    if (utter.Contains("人間"))
                    {
                        utter = utter.Substring(4);
                        utter = utter.Remove(utter.Length - 6);
                        gameData.Tag += "霊能結果 ";
                        gameData.Guess += Complement(utter) + ":人間 ";
                    }
                    if (utter.Contains("人狼"))
                    {
                        utter = utter.Substring(4);
                        utter = utter.Remove(utter.Length - 6);
                        gameData.Tag += "霊能結果 ";
                        gameData.Guess += Complement(utter) + ":人狼 ";
                    }
                }
                else if (Regex.IsMatch(utter, "昨日.+を護衛し、襲撃が無かったので、.+は人間だ。"))
                {
                    gameData.Tag += "護衛結果 ";
                    gameData.Guess += PlayerNameMatch(utter) + " ";
                }
                else if (Regex.IsMatch(utter, "今日の占い先は.+に仮決定します。"))
                {
                    utter = utter.Substring(7);
                    utter = utter.Remove(utter.Length - 8);
                    gameData.Tag += "占い先決定 ";
                    gameData.Guess += "占い仮本決定: " + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, ".+の占い先は、.+に仮決定します。"))
                {
                    string str1 = PlayerNameMatch(utter);
                    string str2 = PlayerNameMatch(utter.Substring(str1.Length));
                    gameData.Tag += "占い先決定 ";
                    gameData.Guess += "占い先仮決定:" + Complement(str1) + ":" + Complement(str2) + " ";
                }
                else if (Regex.IsMatch(utter, "占い師は、今夜.+を占ってください。"))
                {
                    utter = utter.Substring(7);
                    utter = utter.Remove(utter.Length - 9);
                    gameData.Tag += "占い先決定 ";
                    gameData.Guess += "占い先本決定: " + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, ".+は、.+を占ってください。"))
                {
                    string str1 = PlayerNameMatch(utter);
                    string str2 = PlayerNameMatch(utter.Substring(str1.Length));
                    gameData.Tag += "占い先決定 ";
                    gameData.Guess += "占い先本決定:" + Complement(str1) + ":" + Complement(str2) + " ";
                }
                else if (Regex.IsMatch(utter, "今日の投票先は、.+に仮決定します。"))
                {
                    utter = utter.Substring(8);
                    utter = utter.Remove(utter.Length - 8);
                    gameData.Tag += "吊り先決定 ";
                    gameData.Guess += "吊り先仮決定: " + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, ".+の投票先は.+に仮決定します。"))
                {
                    string str1 = PlayerNameMatch(utter);
                    string str2 = PlayerNameMatch(utter.Substring(str1.Length));
                    gameData.Tag += "吊り先決定 ";
                    gameData.Guess += "吊り先仮決定:" + Complement(str1) + ":" + Complement(str2) + " ";
                }
                else if (Regex.IsMatch(utter, "今日の投票先は、.+に決定します。"))
                {
                    utter = utter.Substring(8);
                    utter = utter.Remove(utter.Length - 7);
                    gameData.Tag += "吊り先決定 ";
                    gameData.Guess += "吊り先本決定: " + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, ".+の投票先は.+に決定します。"))
                {
                    string str1 = PlayerNameMatch(utter);
                    string str2 = PlayerNameMatch(utter.Substring(str1.Length));
                    gameData.Tag += "吊り先決定 ";
                    gameData.Guess += "吊り先本決定:" + Complement(str1) + ":" + Complement(str2) + " ";
                }
                else if (Regex.IsMatch(utter, "私、.+は人狼です。よろしく。"))
                {
                    utter = utter.Substring(2);
                    utter = utter.Remove(utter.Length - 11);
                    gameData.Tag += "人狼CO ";
                    gameData.Guess += Complement(utter) + ":人狼CO ";
                }
                else if (Regex.IsMatch(utter, ".+には.+を騙りCOしてほしい。"))
                {
                    if (utter.Contains("村人"))
                    {
                        gameData.Tag += "CO希望 ";
                        gameData.Guess += Complement(PlayerNameMatch(utter)) + ":村人 ";
                    }
                    else if (utter.Contains("占い師"))
                    {
                        gameData.Tag += "CO希望 ";
                        gameData.Guess += Complement(PlayerNameMatch(utter)) + ":占い師 ";
                    }
                    else if (utter.Contains("霊能者"))
                    {
                        gameData.Tag += "CO希望 ";
                        gameData.Guess += Complement(PlayerNameMatch(utter)) + ":霊能者 ";
                    }
                    else if (utter.Contains("狩人"))
                    {
                        gameData.Tag += "CO希望 ";
                        gameData.Guess += Complement(PlayerNameMatch(utter)) + ":狩人 ";
                    }
                    else if (utter.Contains("狂人"))
                    {
                        gameData.Tag += "CO希望 ";
                        gameData.Guess += Complement(PlayerNameMatch(utter)) + ":狂人 ";
                    }
                    else if (utter.Contains("人狼"))
                    {
                        gameData.Tag += "CO希望 ";
                        gameData.Guess += Complement(PlayerNameMatch(utter)) + ":人狼 ";
                    }
                }
                else if (Regex.IsMatch(utter, "今夜は.+を襲撃しようと思う。"))
                {
                    utter = utter.Substring(3);
                    utter = utter.Remove(utter.Length - 10);
                    gameData.Tag += "襲撃希望 ";
                    gameData.Guess = "襲撃意思:" + Complement(utter) + " ";
                }
                else if (Regex.IsMatch(utter, ".+は.+に投票した。"))
                {
                    string str1 = PlayerNameMatch(utter);
                    string str2 = PlayerNameMatch(utter.Substring(str1.Length));
                    gameData.Tag = "投票";
                    gameData.Guess += Complement(str1) + ":" + Complement(str2) + " ";
                }
                else if (Regex.IsMatch(utter, ".+、[0-9]+票。"))
                {
                    string str1 = PlayerNameMatch(utter);
                    string str2 = utter.Substring(str1.Length + 2);
                    str2 = str2.Remove(str2.Length - 2);
                    gameData.Tag = "投票結果";
                    gameData.Guess += Complement(str1) + ":" + str2 + " ";
                }
                else if (Regex.IsMatch(utter, ".+は村人達の手により処刑された。"))
                {
                    utter = utter.Remove(utter.Length - 15);
                    gameData.Tag = "処刑結果";
                    gameData.Guess = utter;
                }
                else if (Regex.IsMatch(utter, ".+は、.+が.+であることを占った。"))
                {
                    string str1 = PlayerNameMatch(utter);
                    string str2 = PlayerNameMatch(utter.Substring(str1.Length));
                    if (utter.Contains("人間"))
                    {
                        gameData.Tag = "占い ";
                        gameData.Guess += Complement(str1) + ":" + Complement(str2) + ":人間 ";
                    }
                    else if (utter.Contains("人狼"))
                    {
                        gameData.Tag = "占い";
                        gameData.Guess += Complement(str1) + ":" + Complement(str2) + ":人狼 ";
                    }
                }
                else if (Regex.IsMatch(utter, ".+は、.+を守っている。"))
                {
                    string str1 = PlayerNameMatch(utter);
                    string str2 = PlayerNameMatch(utter.Substring(str1.Length));
                    gameData.Tag = "狩人";
                    gameData.Guess += Complement(str1) + ":" + Complement(str2) + " ";
                }
                else if (Regex.IsMatch(utter, "次の日の朝、.+が無残な姿で発見された。"))
                {
                    utter = utter.Substring(6);
                    utter = utter.Remove(utter.Length - 12);
                    gameData.Tag = "襲撃結果";
                    gameData.Guess = utter;
                }
                else if (Regex.IsMatch(utter, "今日は犠牲者がいないようだ。人狼は襲撃に失敗したのだろうか。"))
                {
                    gameData.Tag = "襲撃結果";
                    gameData.Guess = "失敗";
                }
                else if (Regex.IsMatch(utter, "全ての人狼を退治した……。人狼に怯える日々は去ったのだ！"))
                {
                    gameData.Tag = "ゲーム結果";
                    gameData.Guess = "村勝利";
                }
                else if (Regex.IsMatch(utter, "もう人狼に抵抗できるほど村人は残っていない……。人狼は残った村人を全て食らい、別の獲物を求めてこの村を去っていった。"))
                {
                    gameData.Tag = "ゲーム結果";
                    gameData.Guess = "人狼勝利";
                }
            }
            return gameData;
        }

        public GameData convertTagToUtterance(GameData gameData)
        {
            string[] datas = gameData.Tag.Split(' ');
            foreach(string data in datas)
            {
                switch (data)
                {
                    case "CO":
                        convertTagCO(gameData);
                        break;
                    case "考察":
                        convertTagGuess(gameData);
                        break;
                    case "希望":
                        convertTagDisire(gameData);
                        break;
                    case "確認":
                        convertTagConfirm(gameData);
                        break;
                    case "信頼度":
                        convertTagReliability(gameData);
                        break;
                    case "ライン":
                        convertTagLine(gameData);
                        break;
                    case "質問":
                        convertTagQuestion(gameData);
                        break;
                    case "同調":
                        convertTagAgree(gameData);
                        break;
                    case "反発":
                        break;
                    case "占い結果":
                        convertTagResultOfFortune(gameData);
                        break;
                    case "霊能結果":
                        convertTagResultOfPsychic(gameData);
                        break;
                    case "護衛結果":
                        convertTagResultOfBodyGuard(gameData);
                        break;
                    case "占い先決定":
                        convertTagDecisionFortune(gameData);
                        break;
                    case "吊り先決定":
                        convertTagDecisionVote(gameData);
                        break;
                    case "投票":
                        convertTagVote(gameData);
                        break;
                    case "投票結果":
                        convertTagResultOfVote(gameData);
                        break;
                    case "処刑結果":
                        convertTagResultOfExecute(gameData);
                        break;
                    case "占い":
                        convertTagFortune(gameData);
                        break;
                    case "霊能":
                        convertTagPsychic(gameData);
                        break;
                    case "狩人":
                        convertTagBodyGuard(gameData);
                        break;
                    case "襲撃結果":
                        convertTagResultOfRaid(gameData);
                        break;
                    case "ゲーム結果":
                        convertTagResultOfGame(gameData);
                        break;
                    case "人狼CO":
                        convertTagWolfCO(gameData);
                        break;
                    case "CO希望":
                        convertTagDisireCO(gameData);
                        break;
                    case "襲撃希望":
                        convertTagDisireRaid(gameData);
                        break;
                    case "Skip":
                        gameData.Utterance = "Skip";
                        break;
                    case "Over":
                        gameData.Utterance = "Over";
                        break;
                }
            }

            return gameData;
        }

        public void convertTagCO(GameData gameData)
        {
            string[] values = gameData.Guess.Split(' ');
            foreach (string value in values)
            {
                if (value.Contains("CO") == true)
                {
                    string[] co = value.Split(':');
                    if (co[1].Contains("非"))
                    {
                        co[1] = co[1].Remove(0, 1);
                        co[1] = co[1].Substring(0, co[1].Length - 2);
                        gameData.Utterance += "私の役職は" + co[1] + "ではありません。 ";
                    }
                    else
                    {
                        co[1] = co[1].Substring(0, co[1].Length - 2);
                        gameData.Utterance += "私の役職は" + co[1] + "です。 ";
                    }
                }
            }
        }

        public void convertTagGuess(GameData gameData)
        {
            string[] values = gameData.Guess.Split(' ');
            foreach (string value in values)
            {
                if (value.Contains("CO") != true)
                {
                    string[] guess = value.Split(':');
                    if (guess[0].Length == 1)
                    {
                        guess[0] = Abbreviation(guess[0]);
                        if (guess[1].Contains("非"))
                        {
                            guess[1] = guess[1].Remove(0, 1);
                            if (guess[1].Contains("寄り"))
                            {
                                gameData.Utterance += guess[0] + "の役職は" + guess[1] + "ではないと思います。 ";
                            }
                            else
                            {
                                gameData.Utterance += guess[0] + "の役職は" + guess[1] + "ではないと思います。 ";
                            }
                        }
                        else
                        {
                            if (guess[1].Contains("寄り"))
                            {
                                gameData.Utterance += guess[0] + "の役職は" + guess[1] + "だと思います。 ";
                            }
                            else
                            {
                                gameData.Utterance += guess[0] + "の役職は" + guess[1] + "だと思います。 ";
                            }
                        }
                    }
                }
            }
        }

        public void convertTagDisire(GameData gameData)
        {
            if (gameData.Disire.Contains("占い希望"))
            {
                string[] values = gameData.Disire.Split(' ');
                foreach(string value in values)
                {
                    string[] disire = value.Split(':');
                    if(disire[0] == "占い希望")
                    {
                        while(disire[1] != "")
                        {
                            if (disire[1].Substring(0, 1) == "非")
                            {
                                disire[1] = disire[1].Remove(0, 1);
                                gameData.Utterance += "占い師には" + disire[1] + "以外を占ってほしい。 ";
                                disire[1] = disire[1].Remove(0, 1);
                            }
                            else
                            {
                                gameData.Utterance += "占い師には" + disire[1] + "を占ってほしい。 ";
                                disire[1] = disire[1].Remove(0, 1);
                            }
                        }
                    }
                }
            }
            if (gameData.Disire.Contains("吊り希望"))
            {
                string[] values = gameData.Disire.Split(' ');
                foreach (string value in values)
                {
                    string[] disire = value.Split(':');
                    if (disire[0] == "吊り希望")
                    {
                        while (disire[1] != "")
                        {
                            if (disire[1].Substring(0, 1) == "非")
                            {
                                disire[1] = disire[1].Remove(0, 1);
                                gameData.Utterance += disire[1] + "以外に投票してほしい。 ";
                                disire[1] = disire[1].Remove(0, 1);
                            }
                            else
                            {
                                gameData.Utterance += disire[1] + "に投票してほしい。 ";
                                disire[1] = disire[1].Remove(0, 1);
                            }
                        }
                    }
                }
            }
        }

        public void convertTagConfirm(GameData gameData)
        {
            string[] values = gameData.Confirm.Split(' ');
            foreach(string value in values)
            {
                string[] confirm = value.Split(':');
                if (confirm[1].Contains("CO"))
                {
                    confirm[0] = Abbreviation(confirm[0]);
                    if (confirm[1].Contains("非"))
                    {
                        confirm[1] = confirm[1].Remove(0, 1);
                        gameData.Utterance += confirm[0] + "の非" + confirm[1] + "COを確認しました。 ";
                    }
                    else
                    {
                        gameData.Utterance += confirm[0] + "の" + confirm[1] + "COを確認しました。 ";
                    }
                }
                if (confirm[0].Contains("占い確認"))
                {
                    confirm[1] = Abbreviation(confirm[1]);
                    confirm[2] = Abbreviation(confirm[2]);
                    gameData.Utterance += confirm[1] + "の" + confirm[2] + "を占って" + confirm[3] + "の結果を確認しました。 ";
                }
                if (confirm[0].Contains("霊能確認"))
                {
                    confirm[1] = Abbreviation(confirm[1]);
                    confirm[2] = Abbreviation(confirm[2]);
                    gameData.Utterance += confirm[1] + "の" + confirm[2] + "を霊能して" + confirm[3] + "の結果を確認しました。 ";
                }
                if (confirm[0].Contains("吊り先確認"))
                {
                    confirm[1] = Abbreviation(confirm[1]);
                    gameData.Utterance += confirm[1] + "吊り確認しました。 ";
                }
                if (confirm[0].Contains("占い先確認"))
                {
                    confirm[1] = Abbreviation(confirm[1]);
                    gameData.Utterance += "占い先指定" + confirm[1] + "確認しました。 ";
                }
            }
        }

        public void convertTagReliability (GameData gameData)
        {
            string[] values = gameData.Reliability.Split(' ');
            foreach (string value in values)
            {
                string[] rely = value.Split(':');
                if (rely[1] == "信頼度高")
                {
                    rely[0] = Abbreviation(rely[0]);
                    gameData.Utterance += rely[0] + "のことは信頼できるとおもう。 ";
                }
                else if (rely[1] == "信頼度低")
                {
                    rely[0] = Abbreviation(rely[0]);
                    gameData.Utterance += rely[0] + "のことは信頼できない。 ";
                }
            }
        }

        public void convertTagLine (GameData gameData)
        {
            string[] values = gameData.Line.Split(' ');
            foreach (string value in values)
            {
                string[] vs = value.Split(':');
                if (vs[0].Contains("非"))
                {
                    if (vs[1].Length == 2)
                    {
                        gameData.Utterance += Abbreviation(vs[1].Substring(0, 1)) + "と" + Abbreviation(vs[1].Substring(1, 2)) + "は両狼出ないと思う。 ";
                    }
                }
                else
                {
                    if (vs[1].Length == 2)
                    {
                        gameData.Utterance += Abbreviation(vs[1].Substring(0, 1)) + "と" + Abbreviation(vs[1].Substring(1, 2)) + "はつながっていると思う。 ";
                    }
                }
            }
        }

        public void convertTagQuestion(GameData gameData)
        {

        }

        public void convertTagAgree(GameData gameData)
        {
            string[] values = gameData.Questtion.Split(' ');
            foreach (string value in values)
            {
                if (value.Contains("同調") == true)
                {
                    string[] agree = value.Split(':');
                    agree[1] = agree[1].Substring(0, 1);
                    gameData.Utterance += Abbreviation(agree[1]) + "の発言に同意する。 ";
                }
            }
        }

        public void converTagOppose(GameData gameData)
        {
            string[] values = gameData.Questtion.Split(' ');
            foreach (string value in values)
            {
                string[] oppose = value.Split(':');
                if (oppose[0].Contains("反発") == true)
                {
                    oppose[1] = oppose[1].Substring(0, 1);
                    gameData.Utterance += Abbreviation(oppose[1]) + "の発言には反対だ。 ";
                }
            }
        }

        public void convertTagResultOfFortune(GameData gameData)
        {
            string[] values = gameData.Guess.Split(':');
            values[0] = Abbreviation(values[0]);
            if (values[0] == null) return;
            switch (values[1])
            {
                case "人間":
                    gameData.Utterance += values[0] + "を占った結果、人間だった。 ";
                    break;

                case "人狼":
                    gameData.Utterance += values[0] + "を占った結果、人狼だった。 ";
                    break;
            }
        }

        public void convertTagResultOfPsychic(GameData gameData)
        {
            string[] values = gameData.Guess.Split(':');
            values[0] = Abbreviation(values[0]);
            if (values[0] == null) return;
            switch (values[1])
            {
                case "人間":
                    gameData.Utterance += "処刑した"+ values[0] + "は人間だった。 ";
                    break;

                case "人狼":
                    gameData.Utterance += "処刑した" + values[0] + "は人狼だった。 ";
                    break;
            }
        }

        public void convertTagResultOfBodyGuard(GameData gameData)
        {
            foreach (string data in gameData.Guess.Split(' '))
            {
                string[] values = data.Split(':');
                if (values[0] == "護衛結果")
                {
                    gameData.Utterance += "昨日" + values[1] + "を護衛し、襲撃が無かったので、" + values[1] + "は人間だ。 ";
                }
            }
        }

        public void convertTagDecisionFortune(GameData gameData)
        {
            string[] values = gameData.Guess.Split(' ');
            foreach (string value in values)
            {
                if (value.Contains("占い先仮決定"))
                {
                    string[] decifortune = value.Split(':');
                    if (decifortune.Length < 3)
                    {
                        while (decifortune[1] != "")
                        {
                            gameData.Utterance += "今夜の占い先は" + Abbreviation(decifortune[1]) + "に仮決定します。 ";
                            decifortune[1] = decifortune[1].Remove(0, 1);
                        }
                    }
                    else
                    {
                        while (decifortune[2] != "")
                        {
                            gameData.Utterance += Abbreviation(decifortune[1]) + "の占い先は" + Abbreviation(decifortune[2]) + "に仮決定します。 ";
                            decifortune[2] = decifortune[2].Remove(0, 1);
                        }
                    }
                }
                else if (value.Contains("占い先本決定"))
                {
                    string[] decifortune = value.Split(':');
                    if (decifortune.Length < 3)
                    {
                        while (decifortune[1] != "")
                        {
                            gameData.Utterance += "占い師は、今夜" + Abbreviation(decifortune[1]) + "を占ってください。 ";
                            decifortune[1] = decifortune[1].Remove(0, 1);
                        }
                    }
                    else
                    {
                        while (decifortune[2] != "")
                        {
                            gameData.Utterance += Abbreviation(decifortune[1]) + "は、今夜" + Abbreviation(decifortune[2]) + "を占ってください。 ";
                            decifortune[2] = decifortune[2].Remove(0, 1);
                        }
                    }
                }
                else break;
            }
        }

        public void convertTagDecisionVote(GameData gameData)
        {
            string[] values = gameData.Guess.Split(' ');
            foreach (string value in values)
            {
                if (value.Contains("吊り先仮決定"))
                {
                    string[] decivote = value.Split(':');
                    if (decivote.Length < 3)
                    {
                        while (decivote[1] != "")
                        {
                            gameData.Utterance += "今日の投票先は、" + Abbreviation(decivote[1]) + "に仮決定します。 ";
                            decivote[1] = decivote[1].Remove(0, 1);
                        }
                    }
                    else
                    {
                        while (decivote[2] != "")
                        {
                            gameData.Utterance += Abbreviation(decivote[1]) + "の投票先は、" + Abbreviation(decivote[2]) + "に仮決定します。 ";
                            decivote[2] = decivote[2].Remove(0, 1);
                        }
                    }
                }
                else if (value.Contains("吊り先本決定"))
                {
                    string[] decivote = value.Split(':');
                    if (decivote.Length < 3)
                    {
                        while (decivote[1] != "")
                        {
                            gameData.Utterance += "今日の投票先は、" + Abbreviation(decivote[1]) + "に決定します。 ";
                            decivote[1] = decivote[1].Remove(0, 1);
                        }
                    }
                    else
                    {
                        while (decivote[2] != "")
                        {
                            gameData.Utterance += Abbreviation(decivote[1]) + "の投票先は、" + Abbreviation(decivote[2]) + "に決定します。 ";
                            decivote[2] = decivote[2].Remove(0, 1);
                        }
                    }
                }
                else break;
            }
        }

        public void convertTagVote(GameData gameData)
        {
            string[] values = gameData.Guess.Split(' ');
            foreach(string value in values)
            {
                if(value != "")
                {
                    string[] vote = value.Split(':');
                    gameData.Utterance += Abbreviation(vote[0]) + "は" + Abbreviation(vote[1]) + "に投票した。 ";
                }
            }
        }

        public void convertTagResultOfVote(GameData gameData)
        {
            string[] values = gameData.Guess.Split(' ');
            foreach (string value in values)
            {
                string[] vote = value.Split(':');
                gameData.Utterance += Abbreviation(vote[0]) + "、" + Abbreviation(vote[1]) + "票。 ";
            }
        }

        public void convertTagResultOfExecute(GameData gameData)
        {
            gameData.Utterance += gameData.Guess + "は村人達の手により処刑された。";
        }

        public void convertTagFortune(GameData gameData)
        {
            string[] values = gameData.Guess.Split(':');
            switch (values[2])
            {
                case "人間":
                    gameData.Utterance += Abbreviation(values[0]) + "は、" + Abbreviation(values[1]) + "が人間であることを占った。 ";
                    break;
                case "人狼":
                    gameData.Utterance += Abbreviation(values[0]) + "は、" + Abbreviation(values[1]) + "が人狼であることを占った。 ";
                    break;
                default:
                    break;
            }
        }

        public void convertTagPsychic(GameData gameData)
        {
            string[] values = gameData.Guess.Split(':');
            switch (values[2])
            {
                case "人間":
                    gameData.Utterance += Abbreviation(values[0]) + "の霊能結果、" + Abbreviation(values[1]) + "が人間であった。 ";
                    break;
                case "人狼":
                    gameData.Utterance += Abbreviation(values[0]) + "の霊能結果、" + Abbreviation(values[1]) + "が人狼であった。 ";
                    break;
                default:
                    break;
            }
        }

        public void convertTagBodyGuard(GameData gameData)
        {
            string[] values = gameData.Guess.Split(':');
            gameData.Utterance += Abbreviation(values[0]) + "は、" + Abbreviation(values[1]) + "を守っている。 ";
        }

        public void convertTagResultOfRaid(GameData gameData)
        {
            if (gameData.Guess != "失敗")
            {
                gameData.Utterance += "次の日の朝、" + gameData.Guess + "が無残な姿で発見された。 ";
            }
            else
            {
                gameData.Utterance += "今日は犠牲者がいないようだ。人狼は襲撃に失敗したのだろうか。 ";
            }
        }

        public void convertTagResultOfGame(GameData gameData)
        {
            if (gameData.Guess.Contains("村"))
            {
                gameData.Utterance = "全ての人狼を退治した……。人狼に怯える日々は去ったのだ！ ";
            }
            else
            {
                gameData.Utterance = "もう人狼に抵抗できるほど村人は残っていない……。" 
                    + "人狼は残った村人を全て食らい、別の獲物を求めてこの村を去っていった。 ";
            }
        }

        public void convertTagWolfCO(GameData gameData)
        {
            string[] values = gameData.Guess.Split(' ');
            foreach (string value in values)
            {
                if(value != "")
                {
                    string[] disire = value.Split(':');
                    gameData.Utterance += "私、" + Abbreviation(disire[0]) + "は人狼です。よろしく。 ";
                }
            }
        }

        public void convertTagDisireCO(GameData gameData)
        {
            string[] values = gameData.Guess.Split(' ');
            foreach (string value in values)
            {
                if (value != "")
                {
                    string[] disire = value.Split(':');
                    gameData.Utterance += Abbreviation(disire[0]) + "には" + disire[1] + "を騙りCOしてほしい。 ";
                }
            }
        }

        public void convertTagDisireRaid(GameData gameData)
        {
            string[] values = gameData.Guess.Split(' ');
            foreach(string value in values)
            {
                if (value != "")
                {
                    string[] disire = value.Split(':');
                    gameData.Utterance += "今日は" + Abbreviation(disire[1]) + "を襲撃しようと思う。 ";
                }
            }
        }

        /// <summary>
        /// テキストに含むプレイヤー名を返す
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string PlayerNameMatch(string str)
        {
            if (str.Contains("少女リーザ"))
            {
                return "少女リーザ";
            }
            else if (str.Contains("ならず者ディーター"))
            {
                return "ならず者ディーター";
            }
            else if (str.Contains("木こりトーマス"))
            {
                return "木こりトーマス";
            }
            else if (str.Contains("羊飼いカタリナ"))
            {
                return "羊飼いカタリナ";
            }
            else if (str.Contains("少年ペーター"))
            {
                return "少年ペーター";
            }
            else if (str.Contains("負傷兵シモン"))
            {
                return "負傷兵シモン";
            }
            else if (str.Contains("神父ジムゾン"))
            {
                return "神父ジムゾン";
            }
            else if (str.Contains("司書クララ"))
            {
                return "司書クララ";
            }
            else if (str.Contains("宿屋の女主人レジーナ"))
            {
                return "宿屋の女主人レジーナ";
            }
            else if (str.Contains("青年ヨアヒム"))
            {
                return "青年ヨアヒム";
            }
            else if (str.Contains("パン屋オットー"))
            {
                return "パン屋オットー";
            }
            else if (str.Contains("行商人アルビン"))
            {
                return "行商人アルビン";
            }
            else if (str.Contains("旅人ニコラス"))
            {
                return "旅人ニコラス";
            }
            else if (str.Contains("老人モーリッツ"))
            {
                return "老人モーリッツ";
            }
            else if (str.Contains("シスターフリーデル"))
            {
                return "シスターフリーデル";
            }

            return null;
        }

        /// <summary>
        /// キャラ名一文字の略称を開く
        /// </summary>
        public string Abbreviation(string abb)
        {
            string charaname = null;
            switch (abb)
            {
                case "楽":
                    charaname = "楽天家ゲルト";
                    break;
                case "長":
                    charaname = "村長ヴァルター";
                    break;
                case "老":
                    charaname = "老人モーリッツ";
                    break;
                case "神":
                    charaname = "神父ジムゾン";
                    break;
                case "樵":
                    charaname = "木こりトーマス";
                    break;
                case "旅":
                    charaname = "旅人ニコラス";
                    break;
                case "者":
                    charaname = "ならず者ディーター";
                    break;
                case "年":
                    charaname = "少年ペーター";
                    break;
                case "妙":
                    charaname = "少女リーザ";
                    break;
                case "商":
                    charaname = "行商人アルビン";
                    break;
                case "羊":
                    charaname = "羊飼いカタリナ";
                    break;
                case "屋":
                    charaname = "パン屋オットー";
                    break;
                case "青":
                    charaname = "青年ヨアヒム";
                    break;
                case "娘":
                    charaname = "村娘パメラ";
                    break;
                case "農":
                    charaname = "農夫ヤコブ";
                    break;
                case "宿":
                    charaname = "宿屋の女主人レジーナ";
                    break;
                case "修":
                    charaname = "シスターフリーデル";
                    break;
                case "仕":
                    charaname = "仕立て屋エルナ";
                    break;
                case "書":
                    charaname = "司書クララ";
                    break;
                case "兵":
                    charaname = "負傷兵シモン";
                    break;
                default:
                    break;
            }
            return charaname;
        }

        public string Complement(string comp)
        {
            switch (comp)
            {
                case "楽天家ゲルト":
                    return "楽";
                case "村長ヴァルター":
                    return "長";
                case "老人モーリッツ":
                    return "老";
                case "神父ジムゾン":
                    return "神";
                case "木こりトーマス":
                    return "樵";
                case "旅人ニコラス":
                    return "旅";
                case "ならず者ディーター":
                    return "者";
                case "少年ペーター":
                    return "年";
                case "少女リーザ":
                    return "妙";
                case "行商人アルビン":
                    return "商";
                case "羊飼いカタリナ":
                    return "羊";
                case "パン屋オットー":
                    return "屋";
                case "青年ヨアヒム":
                    return "青";
                case "村娘パメラ":
                    return "娘";
                case "農夫ヤコブ":
                    return "農";
                case "宿屋の女主人レジーナ":
                    return "宿";
                case "シスターフリーデル":
                    return "修";
                case "仕立て屋エルナ":
                    return "仕";
                case "司書クララ":
                    return "書";
                case "負傷兵シモン":
                    return "兵";
                default:
                    return null;
            }
        }
    }
}
