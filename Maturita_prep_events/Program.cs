using System.Security.Cryptography;
using System.Text;

void OnTemperatureExcedeed()
{
    //Console.WriteLine("Teplota byla překročena.");
}

var sensor1 = new TemperatureSensor("David", 15);
sensor1.myEvent += OnTemperatureExcedeed;

sensor1.UpdateTemperature(21);

Func<string, int> action1 = word => word.Length;
//Console.WriteLine(action1("David"));

Action display = () => Console.WriteLine("Hello");
//display();

string plaintext = "Toto je plaintext, který lze bez prolbému šifrovat jak se komu zlíbí";
string key = "key 123, tímto prosím šifruj";

byte[] bytes = Encoding.UTF8.GetBytes(plaintext);
byte[] encryptedBytes = new byte[bytes.Length];
for (int i = 0; i < plaintext.Length; i++)
{
    encryptedBytes[i] = (byte)(bytes[i] ^ key[i % key.Length]);
}

string encryptedText = Encoding.UTF8.GetString(encryptedBytes);
Console.WriteLine(encryptedText);



public delegate void TemperatureExcedeed();
class TemperatureSensor(string Room, double Temperature)
{
    public string Room;
    public double Temperature;

    public event TemperatureExcedeed myEvent;

    public void UpdateTemperature(double temp)
    {
        Temperature = temp;

        if (Temperature > 20)
        {
            myEvent?.Invoke();
        }
    }
}