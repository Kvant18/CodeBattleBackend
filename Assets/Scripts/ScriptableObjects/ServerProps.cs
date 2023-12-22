using UnityEngine;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Net;
using System.Net.Sockets;

[CreateAssetMenu(fileName = "ServerProps", menuName = "CodeBattleBackend/ServerProps", order = 1)]
public class ServerProps : ScriptableObject
{
  private int port = 1337;
  private string Servername = "MainServer";
  private bool Isprotected = true;
  private string password = "password";
  private int playerCount;
  private PanelControl creatorPlayer;


  public PanelControl CreatorPlayer
  {
    set
    {
      if (creatorPlayer != value)
      {
        creatorPlayer = value;
      }
    }
    get
    {
      return creatorPlayer;
    }
  }

  public IPAddress IPAddress
  {
    get
    {
      IPAddress local = IPAddress.Parse("127.0.0.1");
      IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
      foreach (IPAddress ip in host.AddressList)
      {
        if (ip.AddressFamily == AddressFamily.InterNetwork)
        {
          return ip;
        }
      }
      return local;
    }
  }

  public int Port
  {
    get
    {
      return port;
    }
    set
    {
      if (value > 1000 && value < 8000)
      {
        port = value;
      }
      else
      {
        port = 1000;
      }
    }
  }

  public string Name
  {
    get
    {
      return Servername;
    }
    set
    {
      if (!string.IsNullOrEmpty(value) && value.Length >= 6)
      {
        Servername = value;
      }
    }
  }

  public bool Protected
  {
    get
    {
      return Isprotected;
    }
    set
    {
      if (Isprotected != value)
      {
        Isprotected = value;
      }
    }
  }

  public string Password
  {
    get
    {
      using (HashAlgorithm sha = SHA1.Create())
      {
        byte[] textData = Encoding.UTF8.GetBytes(password);
        byte[] hash = sha.ComputeHash(textData);
        return BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);
      }
    }
    set
    {
      if (!string.IsNullOrEmpty(value) && value.Length >= 8)
      {
        password = value;
      }
    }
  }

  public int PlayerCount
  {
    get
    {
      return playerCount;
    }
    set
    {
      if (value >= 2 && value <= 20)
      {
        playerCount = value;
      }
    }
  }

}
