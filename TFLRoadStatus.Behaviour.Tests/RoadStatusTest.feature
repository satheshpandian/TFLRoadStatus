Feature: RoadStatusTest
	Retrieve DisplayName, Severity and Severity Description 
	When correct road id is passed

@displayStatusName
Scenario: Display Name should be displayed
	Given a valid road ID 'A2' is specified
	When the client is run
	Then the road 'displayName' should be displayed


@displayStatusSeverity
Scenario: Display Status Severity
Given a valid road ID 'A2' is specified
When the client is run
Then the road 'statusSeverity' should be displayed as 'Road Status'

@displayStatusSeverityDescription
Scenario: Display Status Severity Description
Given a valid road ID 'A2' is specified
When the client is run
Then the road 'statusSeverityDescription' should be displayed as 'Road Status Description'