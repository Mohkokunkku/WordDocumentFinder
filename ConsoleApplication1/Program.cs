using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// PARAMETRIT ON KOVAKOODATTU KOODIN SISÄÄN JA TULOKSET TULOSTUVAT KONSOLIIN
// TOIMII NYKYISILLÄ PARAMETREILLÄ VAIN COPLA OY:N SISÄVERKOSSA

namespace FileLoop
{
    class LoopFiles
    {
       static int intTarjous = 0;
       static int intMuistio = 0;
       static int intTarkastus = 0;
       static int intSelostus = 0;
       static int intRaportti = 0;
        static int intKansio = 0;
        static int intUrakka = 0;
        static int intKokous = 0;
        static int intDocuments = 0; //DocStarter asiakirjat
        static int intOtherDocuments = 0; //asiakirjat jotka ei välttämättä ole luotu DocStarterilla
        static int intMuut = 0;
        static void Main(string[] args)
        {
            ProcessFiles("Z:\\Copla Tiedostot");
            Console.WriteLine("Valmis!");
            Console.WriteLine("Tarjouksia " + intTarjous);
            Console.WriteLine("Muistioita " + intMuistio);
            Console.WriteLine("Tarkastusasiakirjoja " + intTarkastus);
            Console.WriteLine("Työselostuksia " + intSelostus);
            Console.WriteLine("Raportteja " + intRaportti);
            Console.WriteLine("Urakkalaskenta asiakirjoja " + intUrakka);
            Console.WriteLine("Kansioita " + intKansio);
            Console.WriteLine("Työmaakokousten asiakirjoja " + intKokous);
            Console.WriteLine("Muita luokittelemattomia asiakirjoja " + intMuut);
            Console.Read();
        }

         static void ProcessFiles(string path)
        {
            Stack<string> stack;
            string[] files;
            string[] directories;
            string dir;
            
            stack = new Stack<string>();
            stack.Push(path);
            DateTime year2017 = new DateTime(2017, 1, 1);
            DateTime year2018 = new DateTime(2017, 12, 31);

            while (stack.Count > 0)
            {

                // Pop a directory
                dir = stack.Pop();
                try
                {

                    files = Directory.GetFiles(dir);


                    foreach (string file in files)
                    {

                        // Process each file
                        FileInfo fi = new FileInfo(file);
                        if (fi.Extension == ".docx" || fi.Extension == ".doc") //tarkistaa mikä on tiedostopääte 
                        {


                            if (fi.CreationTime > year2017 && fi.CreationTime < year2018)  //vertaa tiedoston päivämäärää 
                            {
                                string filename = Path.GetFileNameWithoutExtension(fi.FullName).ToLower();
                                if (filename.Contains("copla"))
                                {
                                    intDocuments++; // DocStarterilla luultavasti luotu asiakirja 
                                    
                                    Console.WriteLine("DocStarterilla luotu: "+ filename + "  Yhteensä: " + intDocuments + " asiakirjaa");
                                }
                                else
                                {
                                    intOtherDocuments++; //Asiakirjat jotka ei välttämättä ole luotu DocStarterilla, eivät sisällä Copla-sanaa
                                    Console.WriteLine("Ei välttämättä luotu DocStarterilla: " + filename + "  Yhteensä: " + intOtherDocuments + " asiakirjaa");
                                }                      
                                    
                                //string filename = Path.GetFileNameWithoutExtension(fi.FullName);
                                documentType(filename);
                                    //  Console.WriteLine(fi.CreationTime);
                                                                  
                            }
                        }
                    }



                    directories = Directory.GetDirectories(dir);
                    foreach (string directory in directories)
                    {
                        // Push each directory into stack
                        stack.Push(directory);
                    }
                }
                catch (PathTooLongException e)
                {
                    Console.WriteLine("!!ERROR HANDLER!!: " + e.Message);
                }

            }
        }

         static void documentType(string filename) //Lajittelee asiakirjat eri ryhmiin, ryhmät kovakoodattu Copla Oy:n tarpeen mukaan
        {
            

            if (filename.Contains("tarjoukset"))
            {
                intTarjous++;
                Console.WriteLine("Tarjoukset " + intTarjous);
            }
            else if (filename.Contains("selostus"))
            {
                intSelostus++;
                Console.WriteLine("Selostukset " + intSelostus);
            }
            else if (filename.Contains("muistio"))
            {
                intMuistio++;
                Console.WriteLine("Muistiot " + intMuistio);
            }
            else if (filename.Contains("raportti"))
            {
                intRaportti++;
                Console.WriteLine("Raportit " + intRaportti);
            }
            else if (filename.Contains("tarkastus"))
            {
                intTarkastus++;
                Console.WriteLine("Tarkastukset " + intTarkastus);
            }
            else if (filename.Contains("kokous"))
            {
                intKokous++;
                Console.WriteLine("Työmaakokoukset" + intKokous);
            }
            else if (filename.Contains("urak"))
            {
                intUrakka++;
                Console.WriteLine("Urakkakilpailutukset " + intUrakka);
            }
            else if (filename.Contains("kansio"))
            {
                intKansio++;
                Console.WriteLine("Kansiot " + intKansio);
            }
            else
            {
                intMuut++;
                Console.WriteLine("Muut asiakirjat " + intMuut);
            }


        }

        }
        




    }

