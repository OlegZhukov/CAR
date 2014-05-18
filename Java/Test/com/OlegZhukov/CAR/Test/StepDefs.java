package com.OlegZhukov.CAR.Test;

import org.junit.Assert;

import com.OlegZhukov.CAR.SeamCarver;

import cucumber.api.java.en.Given;
import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;
import edu.princeton.cs.introcs.Picture;

public class StepDefs {
    private SeamCarver sc;

    @Given("^image (.*)$")
    public void givenImage(String inputImageFile) {
        sc = new SeamCarver(new Picture(inputImageFile));
    }

    @When("^I remove (\\d+) vertical and (\\d+) horizontal seams$")
    public void removeSeams(int v, int h) {
        for (int i = 0; i < v; i++)
            sc.removeVerticalSeam(sc.findVerticalSeam());
        for (int i = 0; i < h; i++)
            sc.removeHorizontalSeam(sc.findHorizontalSeam());
    }

    @Then("^I should get image (.*)$")
    public void shouldGetImage(String expectedImageFile) {
        Picture expectedImage = new Picture(expectedImageFile);
        Assert.assertTrue(expectedImage.equals(sc.picture()));
    }
}
