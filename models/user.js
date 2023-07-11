import mongoose from "mongoose";
const { Schema, model } = mongoose;

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
  },
  {
    timestamps: true,
  }
);

export default model("User", userSchema);
