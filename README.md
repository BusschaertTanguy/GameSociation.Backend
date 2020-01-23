# GameSociation.Backend
GameSociation Backend project.

## Project specifications

### Architecture: Each module is divided in an onion architecture

- Domain layer: Core of the application, contains:
  - Entities / Aggregates: Business object identified by an ID
  - Value objects: Business object identified by it's properties, immutable.
  - Repository interfaces: Service used for persisting / retrieving entities
  - Domain events: Used to communicate between domains, as we are using event sourcing, each action should throw an event.
  - Factories: Service for creating entities.
  - Domain services: When an action is not part of 1 specific aggregate, but multiple, this can be hidden behind a domain service.
  
- Application layer: API of the module, instead of using application services, we use CQRS (commands & queries and their respective handlers), contains:
  - Commands & command handlers: Used to change state, no response.
  - Queries & query handlers: Used to ask for state, no state can be changed here.
  - Views: A projected, read only model of our aggregates / entities, this is a DTO without behaviour, and only serves as a representation for our aggregate / entities, as the state of it is not exposed.
  - Services: As we split our applicaiton services into handlers, some more technical related things like creating token, hashing a password, ... are hidden away behind a service.
  - Event handlers: There is a possiblity to catch events that are thrown from this / other domains. This allows us to do cross domain activity without having hard references. This also allows us to chain action while still having only 1 aggregates saved / transaction.

- Infrastructure layer: Used to handle the tech related stuff of a module, like dependency injection mapping.

### Persistence: Event sourcing

I opted for event sourcing because it fits my following needs:
  - A by technology unaffected (or minimally) domain model. Most of the time when using standard ORM's like EF, you have to expose your domain model's fields to map them to your DB (I'm aware of private backing fields in EF Core, but it's to tedious and the error margin is large ). When using ES, your model doesn't have to expose any state, which for me is a major pro.
  - I've always liked to have view / readmodels to represent the state of something, instead of using entities. I guess this is also possible with normal ORM's, but using ES enforces you to create projections for your entities, this only streamlines my work.

To fit my ES needs, I used the MartenDB library. This wraps an event store around PostgreSQL.

To have my modules technology independant, I didn't implement MartenDB into the infrastructure of my modules, by put everything MartenDB related in it's own library, called Library.MartenEventStore.

The project contains:
- Queries: I implement my IQueryProcessor to use a marten read only session, to query my projections from the DB.
- Projections: Here I put the link between my views and the events launched, each event should adapt my view to get it to represent the correct state.
- Repositories: I implement my generic repository to use the marten session, and store events accordingly.
- Configurations: Here I hook up the interfaces and their marten implementation, I also setup my event store here.
