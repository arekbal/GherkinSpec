[![Build status](https://ci.appveyor.com/api/projects/status/github/arekbal/gherkinspec)](https://ci.appveyor.com/project/arekbal/gherkinspec)
# GherkinSpec
Gherkin .feature files specification processor for .net

https://www.nuget.org/packages/GherkinSpec.Core
https://www.nuget.org/packages/GherkinSpec.NUnit
https://www.nuget.org/packages/GherkinSpec.MsTest
https://www.nuget.org/packages/GherkinSpec.XUnit

XUnitTests / NUnitTests / MsTests files are a exemplary "Spec Files" in itself.

Here is basic use case for developers writing and/or implementing unit tests:

1. Prepare *.feature file and place it in separate "Features" directory.
2. Add a new class with same name as your feature and place it into yet another "Specs" directory.
3. Then, depending on your liking:
a) Run your *.feature file contents through class generator: http://arekbal.github.io/gherkin_spec/ and customize it.
b) Write your test class down by hand, deriving it from GherkinSpecBase and marking it with attributes, fitting all scenarios, background or steps. Test will fail as long as these descriptions wouldn't match *.feature file contents. BTW. The idea is to track *.feature file changes with failing test.
4. Now the fun part begins... implement the steps. Your Step("") or Given("") or Whatever("") are "headers" for implementation. In between "Steps" there should be hard asserts. Whenever your code asserts, you should be able to track last step before it broke. General idea was that it should lead to longer test methods with exact progress steps.

Main point: Steps are there to match your .feature file and mark your progress through out testing Scenario.

# MsTest Example
https://github.com/arekbal/GherkinSpec/blob/master/src/GherkinSpec.Tests/MsTests.cs
# XUnit Example
https://github.com/arekbal/GherkinSpec/blob/master/src/GherkinSpec.Tests/XUnitTests.cs
# NUnit Example
https://github.com/arekbal/GherkinSpec/blob/master/src/GherkinSpec.Tests/NUnitTests.cs

# GherkinSpec Generator
http://arekbal.github.io/gherkin_spec

Source for generator is here(different repo): https://github.com/arekbal/arekbal.github.io/tree/master/gherkin_spec




