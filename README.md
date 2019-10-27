# FundConnRec
Things that should be improved :
Logging information - probably by Filters or some other frameworks like Autofac and interceptors
Data Validation - didn't work on that at all
Unit tests - Created one basic unit test just to show frameworks I normally use and how i write them
ConfigurationRepository - which was not propetly injected into PortfolioRepository as i ran into an issue with properties not correctly loaded from appsettings.json (which forced me to go plan with B with IConfiguration)
SecurityRepository and PortfolioRepository - there are few methods not implemented as they were not required to complete the task but would definitely make it better with more time
Also I didn't make methods of getting latest portfolio by ISIN and creating analytical operations - I ran out of time but I would make them in the same way so I hope overall score won't be affected by this :)