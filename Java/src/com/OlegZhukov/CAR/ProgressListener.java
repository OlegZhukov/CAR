/*
 * Content-Aware Resizing Tool.
 *
 * Copyright © 2014, Oleg Zhukov (mailto:mail@OlegZhukov.com)
 * 
 * This software is licensed under GPL 3.0 license.
 */
package com.OlegZhukov.CAR;

@FunctionalInterface
public interface ProgressListener {
    void notifySeamRemoved();
}
