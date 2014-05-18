Feature: Finding and removing seams

  Scenario: Removing a few vertical and horizontal seams
    Given image Test-Input.png
    When I remove 6 vertical and 5 horizontal seams
    Then I should get image Test-Output.png
