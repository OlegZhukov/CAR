/*
 * Content-Aware Resizing Tool.
 *
 * Copyright © 2014, Oleg Zhukov (mailto:mail@OlegZhukov.com)
 *
 * This software is licensed under GPL 3.0 license.
 */
package com.OlegZhukov.CAR.Test;

import java.io.File;

import org.junit.Assert;

import com.OlegZhukov.CAR.CAR;

import cucumber.api.java.After;
import cucumber.api.java.Before;
import cucumber.api.java.en.And;
import cucumber.api.java.en.Given;
import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;
import edu.princeton.cs.introcs.Picture;

public class StepDefs {
    private String inputImageFile;
    private String energyFunc;

    @Before
    public void beforeScenario() {
        energyFunc = "";
    }

    @Given("^image (.*)$")
    public void givenImage(String inputImageFile) {
        this.inputImageFile = inputImageFile;
    }

    @When("^I use the energy function (.*)$")
    public void useEnergyFunc(String energyFunc) {
        this.energyFunc = energyFunc;
    }

    @And("^I resize the image to (.*) width and (.*) height$")
    public void resize(String v, String h) {
        if (energyFunc != "") energyFunc = " -e=" + energyFunc;
        CAR.main(String.format("-w=%s -h=%s%s %s", v, h, energyFunc,
                inputImageFile).split(" "));
    }

    @Then("^I should get image (.*)$")
    public void shouldGetImage(String expectedImageFiles) {
        String[] expectedImageFilesArr =
                expectedImageFiles.split("( or | OR )");
        for (int i = 0; i < expectedImageFilesArr.length; i++)
            if (new Picture(expectedImageFilesArr[i]).equals(new Picture(CAR
                    .getOutputPictureFile()))) {
                Assert.assertTrue(true);
                return;
            }
        Assert.fail();
    }

    @After
    public void afterScenario() {
        new File(CAR.getOutputPictureFile()).delete();
    }
}
