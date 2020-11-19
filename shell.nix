let 
  sources = import ./nix/sources.nix;
  pkgs = import sources.nixpkgs {
    overlays = [ 
      (import ./nix/dotnet-sdk-overlay.nix) 
    ]; 
  };
in pkgs.mkShell {
  buildInputs = with pkgs; [
    dotnet-sdk
    # keep this line if you use bash
    bashInteractive
  ];
}