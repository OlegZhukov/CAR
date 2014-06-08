Feature: Finding and removing seams
  
  Scenario: Removing a few seams with the RGBGradNorm energy function
    Given image Test-Input.png
    When I use the energy function RGBGradNorm
    And I resize the image to 594px width and 395px height
    Then I should get image Test-Output1.png
    
  Scenario: Resizing to 70% with the BrightGradX energy function
    Given image Test-Input.png
    When I use the energy function BrightGradX
    And I resize the image to 70% width and 70% height
    Then I should get image Test-Output2.png or Test-Output2_2.png
    
  Scenario: Resizing to 50% x 300px with the default energy function
    Given image Test-Input.png
    When I resize the image to 50% width and 300px height
    Then I should get image Test-Output3.png or Test-Output3_2.png
    
  Scenario: Resizing to 50% x 300px with the BrightGradNorm energy function
    Given image Test-Input.png
    When I use the energy function BrightGradNorm  
    And I resize the image to 50% width and 300px height
    Then I should get image Test-Output4.png OR Test-Output4_2.png
