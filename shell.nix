let
  sources = import ./nix/sources.nix;
  pkgs = import sources.nixpkgs {
    overlays = [ (import ./nix/dotnet-sdk-overlay.nix) ];
  };
  dotnetCombined = with pkgs.dotnetCorePackages;
    combinePackages [ pkgs.dotnet-sdk pkgs.dotnet-sdk_5 ];
in pkgs.mkShell {
  buildInputs = with pkgs; [
    dotnetCombined
    # keep this line if you use bash
    bashInteractive
  ];
}
