﻿using DbLibrary;
using Newtonsoft.Json;
using Shared;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Server
{
    class LaunchServer
    {
        public static void Main(string[] args)
        {
            ServerConnection connection = new ServerConnection();
        }

        public static void CreateUser(string username, string pass)
        {
            ClientProcessing cp = new ClientProcessing();
            int id = cp.AddActiveUser();
            cp.CreateUser(String.Format("Username:{0}$$Password:{1}$$UserIV:{0}$$",username,pass),id);
        }



        public static void FriendsAddTest(string username1,string username2,int invId)
        {
            ;
        }

        public static void MessageTest()
        {
            ClientProcessing cp = new ClientProcessing();
            int id = cp.AddActiveUser();
            int id1 = cp.AddActiveUser();
            cp.Login("Username:test8$$Password:test1234$$", id);
            cp.Login("Username:test7$$Password:12345678$$", id1);


            Console.WriteLine(cp.ActivateConversation("ConversationID:6$$", id));
            Console.WriteLine(cp.NewMessages("", id));
            Message m = new Message { date = DateTime.Now, message = "test", username = "tar" };
            string s = JsonConvert.SerializeObject(m);
            Console.WriteLine(cp.SendMessage(String.Format("Username:test7$$Data:{0}$$", s), id));
            Console.WriteLine(cp.SendMessage(String.Format("Username:test7$$Data:{0}$$", s), id));
            Console.WriteLine(cp.Notification("", id1));
            Console.WriteLine(cp.Notification("", id1));
            Console.WriteLine(cp.ActivateConversation("ConversationID:6$$", id1));
            Console.WriteLine(cp.NewMessages("", id1));
        }

        public static void DH()
        {
            var Tpar = Security.GenerateParameters();

            // To są Twoje p i g, stringi zawierające 16-stkowo zapisaną liczbę
            string p = Security.GetP(Tpar);
            string g = Security.GetG(Tpar);


            // Tworzysz nowe parametry podając 16 jako drugi argument konstruktora
            var par = new Org.BouncyCastle.Crypto.Parameters.DHParameters(new Org.BouncyCastle.Math.BigInteger(p,16), new Org.BouncyCastle.Math.BigInteger(g,16));
            
            var Akeys = Security.GenerateKeys(par);
            var Bkeys = Security.GenerateKeys(par);

            // Chcąc odwołać się do np publicznego klucza (zapisanego szesnastkowo)
            string x = Security.GetPublicKey(Akeys);
            string y = Security.GetPrivateKey(Bkeys);


            // Ta funkcja jakos PublicKey przyjmuje stringa reprezentującego liczbę 16-stkową, a funkcja GetPublic/PrivateKey to zwraca
            Console.WriteLine(Security.ComputeSharedSecret(Security.GetPublicKey(Akeys), Security.GetPrivateKey(Bkeys), p,g).Length);

            Console.WriteLine(Security.ComputeSharedSecret(Security.GetPublicKey(Bkeys), Security.GetPrivateKey(Akeys), p, g).Length);

        }
    }
}
