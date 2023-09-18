# cloudinteractive-nbconvert++

Convert your Jupyter Notebook along with a fully customizable cover page to PDF.

This tool is designed for those who wish to easily convert their Jupyter Notebooks into PDFs with the added benefit of a customizable HTML-based cover page.

Awesome for your coding assignment.

### Table of Contents
* [Features](#features)
* [Requirements](#requirements)
* [Installation](#installation)
* [How to use](#how-to-use)
* [Dependencies](#dependencies)
* [Showcase](#showcase)




## Features
* Fully customizable HTML-based cover page
* Integration into Visual Studio Code (via task.json)

## Requirements
**Now Supporting linux-x64, Windows-x64, macOS-ARM64! **

This project uses Selenium ChromeDriver for converting HTML to PDF.

Make sure you have installed [Google Chrome](https://www.google.com/chrome/).

#### Step-by-Step
Debian-based Linux distributions:

```bash
wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
sudo apt install ./google-chrome-stable_current_amd64.deb
```

RHEL, CentOS, Fedora:
```bash
wget https://dl.google.com/linux/direct/google-chrome-stable_current_x86_64.rpm
sudo yum install google-chrome-stable_current_x86_64.rpm
```

## Installation

### Automated Installation via Package manager
Go to [Release](https://github.com/Coppermine-SP/cloudinteractive-nbconvert/releases/) and download the latest build and install .deb package.

```bash
apt install ./package.deb
```

### Manual build
1. Clone this repository.
2. Build using Visual Studio or the .NET CLI:
```powershell
dotnet publish -r linux-x64 --self-contained true -p:PublishSingleFile=true
```

Execute ./bin/publish/make_debian_package.sh to create the .deb file.

If you're not using Debian-based Linux distribution, manually copy the binaries.

## How to use
Convert ```test.ipynb``` file without cover page:
```bash
nbconvert++ test.ipynb
```

Convert ```test.ipynb``` file with default cover page:
```bash
nbconvert++ test.ipynb --cover-template="/usr/lib/cloudinteractive-nbconbvert/default.html" --title="My Awesome assignment."
```

Convert ```test.ipynb``` file with your custom cover page:
```bash
nbconvert++ test.ipynb --cover-template="[location of your template]" --title="My Awesome assignment."
```
### Customizing Cover Template
You can either modify the default template or start from an empty HTML document. 


The title will be provided through the URL Query:
```
file://home/coppermine/your_cover_template?title="My Awesome assignment."
```
ChromeDriver will capture the page in A4 size with default margin.

### Customizing Jupyter Notebook Style
To alter the style of the Jupyter Notebook, adjust the jupyter-nbconvert template's CSS file.


This file is typically found at :
```
/home/[User Name]/.local/share/jupyter/nbconvert/templates/lab/static/theme-light.css
```

## Dependencies
* [PDFsharp](https://www.nuget.org/packages/PDFsharp/1.50.5147/) - MIT License
* [Selenium.WebDriver](https://www.nuget.org/packages/Selenium.WebDriver/4.12.4/) - Apache-2.0 License
* [Selenium.WebDriver.ChromeDriver](https://www.nuget.org/packages/Selenium.WebDriver.ChromeDriver/117.0.5938.6200/) - Unlicense
* [System.Text.Encoding.CodePages](https://www.nuget.org/packages/System.Text.Encoding.CodePages/7.0.0/) - MIT License
  
## Showcase 
 <img src="/images/code_sample.png">

 
 <img src="/images/pdf_sample.png">
