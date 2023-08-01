import mongoose from "mongoose";

const { Schema, model } = mongoose;

import questionSchema from "./question.js";

const userSchema = new Schema(
  {
    username: {
      type: String,
      required: true,
      unique: true,
    },
    email: {
      type: String,
      required: true,
      unique: true,
    },
    level: {
      type: String,
      //required: true,
    },
    password: {
      type: String,
      required: true,
    },
    isAdmin: {
      type: Boolean,
      default: false,
    },
    score: {
      type: Number,
      default: 0,
    },
    recoveryCode: {
      type: String,
      default: null,
    },
    recoveryCodeExpiration: {
      type: Date,
      default: null,
    },
    question1: questionSchema,
    question2: questionSchema,
    question3: questionSchema,
  },
  {
    timestamps: true,
  }
);

export default model("User", userSchema);
