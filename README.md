CAR
===
Content-Aware Resizing (CAR) - is a cross-platform bilingual (C#/Java) implementation
of the [Seam carving](http://en.wikipedia.org/wiki/Seam_carving) algorithm.

The Content-Aware Resizing algorithm allows resizing images (jpeg, png and others) while
preserving the significant parts of the image and maintaining their proportions.

The project consists of several sub-projects, each targeted to a specific environment.

## Paint.NET plugin

The latest binaries (dll file) can be downloaded from
http://github.com/OlegZhukov/CAR/releases/download/v2.0/CAR.Paint.NET.zip.

#### Usage

Just copy the `CAR.Paint.NET.dll` library to the `Effects` folder in
the Paint.Net directory (usually `C:\Program Files\paint.net\`).

#### Compiling from sources

The corresponding VS project is located in the *CS\CAR.Paint.NET\* directory.
Before compiling it reroute (if needed) the project references
pointing to Paint.NET dll's.

## Command line tool (Windows/cross-platform, .NET 4.0/Mono 2.8 or higher)

The latest binaries can be downloaded from
http://github.com/OlegZhukov/CAR/releases/download/v1.5/CAR.Win.zip.

#### Usage

To resize *Image.png* to 50% of its current width, type:

`CAR.exe Image.png --width=50%`

To see the usage prompt, just run `CAR.exe` without arguments.

#### Compiling from sources

Compiling `CAR.exe` from sources is as easy as opening *CS/CAR.sln* solution in
Visual Studio and running the compilation.

However, in order to compile and run tests (CAR.Test project) the NuGet plug-in for
Visual Studio is required. Once installed, it will automatically resolve dependencies
and download necessary assemblies. You will also need to configure your favorite
test runner to run the NUnit tests found in CAR.Test.dll.

#### Compiling in MonoDevelop

Again, compiling `CAR.exe` is fairly straightforward in MonoDevelop just like in
Visual Studio.

Precompiled version of `CAR.exe` for Linux (Ubuntu) is available at
http://github.com/OlegZhukov/CAR/releases/download/v1.5/CAR.Mono.Linux.zip.

As for the tests (CAR.Test project), MonoDevelop will need to locate the NUnit
and SpecFlow assemblies. Therefore you might need to add the corresponding `.pc`
files to your `/usr/lib/pkgconfig/` directory.

## Command line tool (Cross-platform, Java 8 or higher)

The latest binaries (class files and necessary jar libraries) can be downloaded
from http://github.com/OlegZhukov/CAR/releases/download/v1.5/CAR.Java.class.zip.

#### Usage

To resize *Image.png* to 50% of its current width, navigate to the directory where
you extracted the downloaded zip file and type (in Windows):

`java -cp "./;./lib/stdlib-package.jar" com.OlegZhukov.CAR.CAR Image.png --width=50%`

To see the usage prompt, just omit the arguments afer "CAR":

`java -cp "./;./lib/stdlib-package.jar" com.OlegZhukov.CAR.CAR`

In Linux use `:` instead of `;`:

```
java -cp "./:./lib/stdlib-package.jar" com.OlegZhukov.CAR.CAR Image.png --width=50%
java -cp "./:./lib/stdlib-package.jar" com.OlegZhukov.CAR.CAR
```

#### Compiling from sources

The simplest way to compile the Java CAR project is to it using the Eclipse IDE. Just
open the Eclipse project in Java folder and run the compilation.
