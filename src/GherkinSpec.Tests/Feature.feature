﻿@hicking
Feature: Serve coffee
    Coffee should not be served until paid for
    Coffee should not be served until the button has been pressed
    If there is no coffee left then money should be refunded

  Background:
    Given a global administrator named 'Greg'
    And a blog named Greg's anti-tax rants
    And a customer named 'Wilson'
    And a blog named 'Expensive Therapy' owned by 'Wilson'

  @billing @bicker @annoy
  Scenario: Buy last coffee
    Given a blog post named "Random" with:
    """
    Some Title, Eh?
    ===============
    Here is the first paragraph of my blog post.
    Lorem ipsum dolor sit amet, consectetur adipiscing
    elit.
    """
    Given there are 1 coffees left in the machine
    And I have deposited 1$
	Given the following people exist:
      | name  | email           | phone |
      | Aslak | aslak@email.com | 123   |
      | Joe   | joe@email.com   | 234   |
      | Bryan | bryan@email.org | 456   |
    When I press the coffee button
    Then I should be served a coffee
	Then the greeting service response will contain one of the following messages:
      |Hello how are you doing?|
      |Welcome to the front door!|
      |How has your day been?|
      |Come right on in!|

  @auto
  Scenario: Check if first article contains G
    Given I go to 'wp.pl'
	Given I open first link from 'text_topnews'

  Scenario: Buy last coffee 2
    Given there are 1 coffees left in the machine
    And I have deposited 1$
	Given the following people exist:
      | name  | email           | phone |
      | Aslak | aslak@email.com | 123   |
      | Joe   | joe@email.com   | 234   |
      | Bryan | bryan@email.org | 456   |
    When I press the coffee button
    Then I should be served a coffee
	Then the greeting service response will contain one of the following messages:
      |Hello how are you doing?|
      |Welcome to the front door!|
      |How has your day been?|
      |Come right on in!|

  Scenario Outline: Eating
    Given there are <start> cucumbers
    When I eat <eat> cucumbers
    Then I should have <left> cucumbers

	Examples: First One
	  | start | eat | left |
	  |  12   |  5  |  7   |
	  |  20   |  5  |  15  |
	Examples: Duda 1
	  | start | eat | left |
	  |  12   |  5  |  7   |
	  |  20   |  5  |  15  |