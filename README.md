# Using CustomOps in ONNX
As an example we use universal sentence encoder 3 (MUSE) that hase SentencepieceOp unknown to pure ONNX
Pretrained MUSE is converted to .onnx format
## Model input
Raw string
## Model output
512-element float array

## Compile and run
```sh
dotnet run '{"instances": [{"inputs":["I have seen the world, done it all, had my cake now"]}, {"inputs":["Diamonds, brilliant, and Bel Air now"]}]}'
```

## Release
```sh
dotnet publish -c Release --self-contained --runtime linux-x64 -o out
```
## Trial with json example
```sh
dotnet <PATH TO DLL>/out/MUSEONNX.dll  '{"instances": [{"inputs":["I have seen the world, done it all, had my cake now"]}, {"inputs":["Diamonds, brilliant, and Bel Air now"]}]}'

```
