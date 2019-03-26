# Project Title

TfL Coding Challenge

## Getting Started

The document is aimed to describe how to use and run Unit tests, Behaviourial Tests and Command line execution

### Prerequisites

Install Visual Studio Community Edition 2017 and open the solution. Because all the packages are deleted, first build the solution to automatically download missing packages and build the solution.

Here is the link to download Visual Studio Community Edition from Microsott - https://imagine.microsoft.com/en-us/Catalog/Product/530

Installing PowerShell please read and install from the link - https://docs.microsoft.com/en-us/powershell/scripting/setup/installing-windows-powershell?view=powershell-6

### Installing

1. Install Visual Studio all default settings
2. Clone the solution from Git
3. Open the solution file TFLRoadStatus.sln
4. Install Powershell
5. Create deploy folder in 'C:\'
	1) Open PowerShell
	2) execute 'pwd' and check if you are in c: drive
	3) if c: drive is the current drive go to e)
	4) execute 'c:'
	5) execute 'cd \' and make 'C:' root current folder
	6) execute 'md deploy'

Updating app_id and app_key

There are app.congig files int the below list of projects and both keys should be updated.
```
Projects to update:
TFLRoadStatus.Behaviour.Tests
TFLRoadStatus
```

Please update app_id and app_key appropriately

## Running the tests

- Open Test Explorer in Visual Studio Community Edition 2017 

```
After the Test Explorer is opened three test projects available are:
- TFLRoadStatus.Behaviour.Tests - Behavioural SpecFlow Tests (BDD)
- TFLRoadStatus.Unit.Tests - Moq and Unit Testing the implementation by decoupling WebRequests and app.config
```

Clicking 'Run All' will run all tests together and also clicking on each project separately and right click 'Context Menu' will appear -> 'Run Selected Tests'.

That command will run all the tests implemented in the project TDD/BDD

## Running from PowerShell

1. Open PowerShell
2. The 'deploy' folder is already created and from PowerShell prompt execute
	1) execute 'c:'
	2) execute 'cd \'
	3) execute 'cd deploy'
3.	Run 'TFLRoadStatus A2' 

```
The expected result is:
PS C:\deploy> .\TFLRoadStatus.exe A2
The status of the A2 is as follows
        Road Status is Good
        Road Status Description is No Exceptional Delays
```
##Assumptions

For the better project quality and decoupling from the external URI where it may not be available for any particular reason I created :

```
TFLRoadStatus.Behaviour.Tests 
TFLRoadStatus.UnitTests
TFLRoadStatus.Models
TFLRoadStatus.Common
```

TFLRoadStatus.Behaviour.Tests is aimed to BDD the application but mocking external URI and the configuration
TFLRoadStatus.UnitTests covers most of the functionalities of the application
TFLRoadStatus.Models contains only the models used in this project and segration of this gives the clear seperation of concerns.
TFLRoadStatus.Common contains the stub method to create the stub for HttpMessageHandler.
```
```
More than 90% in Code Coverage and Test coverage was the goal and reached 80% so far.
## Author

* **Sathesh Pandian Manickavasakam** - *Tfl Codding Challange* - [RoadStatusProcessor](https://github.com/satheshpandian/TFLRoadStatus)

