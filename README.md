<p align="center"><img src="https://i.imgur.com/dA6xa9g.png" alt="SolidStack"></p>

<br>

**Quick links**:
[Docs][docs-url],
[Changelog][changelog-url],
[Nuget][nuget-packages-url]

[![Build status][build-status-badge]][build-url]
[![GitHub release][github-release-badge]][license-url]
[![MIT license][license-badge]][license-url]

<br>

# What is SolidStack?

SolidStack is a .NET framework consisting of several well-divided NuGet packages that allow you to focus on business logic by providing an implementation of very general design patterns useful in every kind of applications such as the [*Option Pattern*][option-pattern-url], the [*Builder Pattern*][builder-pattern-url], the [*Repository Pattern*][repository-pattern-url], the [*Unit of Work Pattern*][unit-of-work-pattern-url] and so more. In short, if you're tired of duplicated and unreadable code, this framework is for you!

## Available NuGet packages

### SolidStack.Core

The `SolidStack.Core` namespace is the central point of all SolidStack packages, providing very generic concepts that are useful in any type of program, such as object construction, object equality, code flow and more.

Package | Description
------- | -----------
[SolidStack.Core.Guards][solidstack.core.guards-page] | `SolidStack.Core.Guards`  is an extremely simple, unambiguous and lightweight [*guard clause*][guard-clauses-url] library.
[SolidStack.Core.Flow][solidstack.core.flow-page] | `SolidStack.Core.Flow` focuses on encapsulating the branching logic of your code so you can write a linear and much more readable code flow without having to deal with exceptions, null checks and unnecessary conditions.
SolidStack.Core.Equality (coming soon...) | `SolidStack.Core.Equality` is primarily useful when you have to tweak the equality of an object to implement the [*Value Object Pattern*][value-object-pattern-url]. All you have to do is use one of the provided abstract classes and the complex equality logic will be done for you.
SolidStack.Core.Construction (coming soon...) | `SolidStack.Core.Construction`'s only responsibility is to help you construct objects. You can use the [*Builder Pattern*][builder-pattern-url] provided implementation to build complex objects in a fluent way.

### SolidStack.Domain (coming soon...)

The `SolidStack.Domain` namespace make it easier when it's time to implement business logic by providing a strong basic implementation of the [*domain-driven design*][ddd-url] philosophy.

### SolidStack.Infrastructure (coming soon...)

The `SolidStack.Infrastructure` namespace provides a general implementation of many features that are not releated to business logic like memory deallocation, time, encryption, serialization and so on.

### SolidStack.Persistence (coming soon...)

The `SolidStack.Testing` namespace abstracts how the data is stored so that you can simply switch the way your application stores your data to almost everything such as a SQL database, a NoSQL database, local files, in memory and many more without affecting the rest of your application.

### SolidStack.Testing (coming soon...)

The `SolidStack.Testing` namespace provide classes to help you build highly reusable and readable tests so you can simply write tests on a given interface once and then validate each concrete type of that interface with those same tests.

# How do I get started?

Check out our [wiki][docs-url] to explore the documentation provided for the packages you want. There is documentation on each package available!

# Where can I get it?

First, [install NuGet][nuget-install-url].  Then, install the required NuGet packages avalaible on [nuget.org][nuget-softframe-url] using the package manager console:

```
PM> Install-Package <PackageName>
```

# I have an issue...

First, you can check if your issue has already been tracked [here][issues-url].

Otherwise, you can check if it's already fixed by pulling the [develop branch][develop-branch-url], building the solution and then using the generated DLL files direcly in your project.

If you still hit a problem, please document it and post it [here][new-issue-url].

# How can I contribute?

Contributors are greatly appreciated, just follow the following steps:

1. **Fork** the repository
2. **Clone**  the project to your own machine
3. **Commit**  changes to your own branch
4. **Push**  your work back up to your fork
5. **Create a  pull request**  containing a description and your work and what is the motivation behind it
6. **Submit your pull request** so that we can review your changes

# License

SolidStack is Copyright © 2018 SoftFrame under the [MIT license][license-url].

<!-- Resources: -->
[builder-pattern-url]: http://www.codinghelmet.com/?path=howto/advances-in-applying-the-builder-design-pattern
[build-status-badge]: https://img.shields.io/travis/softframe/solidstack.svg?style=flat-square
[build-url]: https://travis-ci.org/softframe/solidstack
[changelog-url]: https://github.com/softframe/solidstack/releases
[develop-branch-url]: https://github.com/softframe/solidstack/tree/develop
[docs-url]: https://github.com/softframe/solidstack/wiki
[ddd-url]: https://en.wikipedia.org/wiki/Domain-driven_design
[github-release-badge]: https://img.shields.io/github/release/softframe/solidstack.svg?style=flat-square
[issues-url]: https://github.com/softframe/solidstack/issues
[github-release-url]: https://github.com/softframe/solidstack/releases
[guard-clauses-url]: https://medium.com/softframe/what-are-guard-clauses-and-how-to-use-them-350c8f1b6fd2
[license-badge]: https://img.shields.io/badge/license-MIT-orange.svg?style=flat-square
[license-url]: https://github.com/softframe/solidstack/blob/master/LICENSE
[new-issue-url]: https://github.com/softframe/solidstack/issues/new
[nuget-packages-url]: https://www.nuget.org/profiles/softframe
[nuget-install-url]: http://docs.nuget.org/docs/start-here/installing-nuget
[option-pattern-url]: http://www.codinghelmet.com/?path=howto/understanding-the-option-maybe-functional-type
[repository-pattern-url]: https://martinfowler.com/eaaCatalog/repository.html
[solidstack.core.guards-page]: https://github.com/softframe/solidstack/wiki/SolidStack.Core.Guards
[solidstack.core.flow-page]: https://github.com/softframe/solidstack/wiki/SolidStack.Core.Flow
[unit-of-work-pattern-url]: https://martinfowler.com/eaaCatalog/unitOfWork.html
[value-object-pattern-url]: https://martinfowler.com/bliki/ValueObject.html
