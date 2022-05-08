using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Onnx;
using System.Text.Json;
using Microsoft.ML.OnnxRuntime;
using Newtonsoft.Json;
using Microsoft.ML.OnnxRuntime.Tensors;

public class Root{
        public List<DataClass> instances { get; set; }
    }

public class DataClass {
        public string [] inputs { get; set; }
    }
    
    
class Program {

static string ONNX_MODEL_PATH = "muse3.onnx";

static void Main(string[] args){
    //string jsonString = @"{""instances"": [{""inputs"":[""I have seen the world, done it all, had my cake now""]}, {""inputs"":[""Diamonds, brilliant, and Bel Air now""]}]}";
    
    Root parsed = JsonConvert.DeserializeObject<Root>(args[0]);  
    
    var sessionOptions = new Microsoft.ML.OnnxRuntime.SessionOptions(); 
    
    sessionOptions.RegisterCustomOpLibraryV2(System.Environment.CurrentDirectory +"/libortcustomops.so", out var libraryHandle);
    
    var session = new Microsoft.ML.OnnxRuntime.InferenceSession(
    ONNX_MODEL_PATH
   , sessionOptions
    );
   
    foreach (var el in parsed.instances){
        var inputs = new List<NamedOnnxValue>() {};
        inputs.Add(NamedOnnxValue.CreateFromTensor<string>("inputs", new DenseTensor<string>(el.inputs, new int [] {1})));
        
        var output = session.Run(inputs).ToList().Last().AsTensor<float>();
        
        Console.WriteLine(JsonConvert.SerializeObject(output));
         
        }

  }

}
