# cloudinteractive-nbconvert

Convert your Jupyter Notebook along with a fully customizable cover page to PDF.

## Features
* Fully customizable HTML-based cover page
* Integration into Visual Studio Code (via task.json)

## Requirements
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

## Showcase 
 <img src="https://github.com/Coppermine-SP/cloudinteractive-nbconvert/assets/10647913/8880af25-1cac-478d-8657-0b481eeed67d">

 
 <img src="https://github.com/Coppermine-SP/cloudinteractive-nbconvert/assets/10647913/e26fe91a-25c0-42c5-9eab-7e4f15bee3b9">
