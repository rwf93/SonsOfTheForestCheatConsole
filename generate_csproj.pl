#!/usr/bin/perl -w

use File::Basename;

$mline = <<string_ending_delimiter;
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <AssemblyName>ForestMod</AssemblyName>
    <Description>My first plugin</Description>
    <Version>1.0.0</Version>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
    <LangVersion>latest</LangVersion>
    <RestoreAdditionalProjectSources>
      https://api.nuget.org/v3/index.json;
      https://nuget.bepinex.dev/v3/index.json;
      https://nuget.samboy.dev/v3/index.json
    </RestoreAdditionalProjectSources>
    <RootNamespace>ForestMod</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="BepInEx.Unity.IL2CPP" Version="6.0.0-be.*" IncludeAssets="compile" />
    <PackageReference Include="BepInEx.PluginInfoProps" Version="2.*" />
string_ending_delimiter

my @files = <lib/*>;
foreach my $file (@files) {
  my ($name, $path, $suffix) = fileparse($file, qr'\.[^\.]*');

  $generated = <<string_ending_delimiter;
    <Reference Include="$name">
      <HintPath>$path$name$suffix</HintPath>
    </Reference>
string_ending_delimiter
  $mline = $mline . $generated;
}

$mline = $mline . <<string_ending_delimiter;
  </ItemGroup>
</Project>
string_ending_delimiter

print "$mline\n";