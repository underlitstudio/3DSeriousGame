import mongoose from "mongoose";
const { Schema, model } = mongoose;

const questionSchema = new Schema({
  time: {
    type: String,
    required: true,
  },
  attempts: {
    type: Number,
    default: 0,
  },
});

export default questionSchema;
