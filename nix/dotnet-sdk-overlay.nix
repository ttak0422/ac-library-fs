self: super: 

let
  stdenv = self.pkgs.stdenv;
  sha512 = {
    x86_64-linux  = "934bf29734776331691a4724f2174c4e9d2d1dde160f18397fd01abf0f96f2ec1bdd2874db9f0e25dce6993d527ea9c19031a0e67383fd813dcfcb9552ea0c70";
    aarch64-linux = "2f4f6b7ae55802b0beaf5d62bcb64f23ce132c9e08d8ce0f0af8d9b0b1b2c2629b1d4e805e83f831575c9968a86d1495dfa5292d2592af0cd1ae4a385daf42e7";
    x86_64-darwin = "1af1da3ecc4a7f58f7090c88c0a2b5ec4ddf8fc7e4bba295fa9be263634adfee4ad2e7ff3932c8631576d67cb7ed2580d28a48b5f8741943b393104f54c14542";
  };
  url = {
    x86_64-linux  = "https://download.visualstudio.microsoft.com/download/pr/f65a8eb0-4537-4e69-8ff3-1a80a80d9341/cc0ca9ff8b9634f3d9780ec5915c1c66/dotnet-sdk-3.1.201-linux-x64.tar.gz";
    aarch64-linux = "https://download.visualstudio.microsoft.com/download/pr/98a2e556-bedd-46c8-b3fa-96a9f1eb9556/09f60d50e3cbba0aa16d48ceec9dcb0b/dotnet-sdk-3.1.201-linux-arm64.tar.gz";
    x86_64-darwin = "https://download.visualstudio.microsoft.com/download/pr/0f566b2b-47e6-484d-91e5-96d2e48c0466/342c321a2a040b62e0ecf186c4ec9d12/dotnet-sdk-3.1.201-osx-x64.tar.gz";
  };
in {
  dotnet-sdk = super.dotnet-sdk.overrideAttrs (oldAttrs: rec {
    version = "3.1.201";
    src = super.fetchurl {
      url = url."${stdenv.hostPlatform.system}" or (throw
        "Missing url for host system: ${stdenv.hostPlatform.system}");
      sha512 = sha512."${stdenv.hostPlatform.system}" or (throw
        "Missing hash for host system: ${stdenv.hostPlatform.system}");
    };
  });
}