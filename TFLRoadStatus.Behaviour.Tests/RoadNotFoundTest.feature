Feature: RoadNotFoundTest And ExitCode Test
	When an invalid road ID is passed
	The application has to return HttpStatusCode as 404

@errorCode404
Scenario: HttpErrorCode 404
	Given an invalid road ID 'A223' is passed
	When the client is run
	Then the application should return an informative error

@error
Scenario: Non Zero Exit code 
	Given an invalid road ID 'A223' is passed
	When the client is run
	Then the application should exit with a non-zero system Error code