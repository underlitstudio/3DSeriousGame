import express from "express";
import { body } from "express-validator";

//import multer from "../middlewares/multer-config.js";

import {
  signin,
  signup,
  sendRecoveryCode,
  updatePassword,
  verifyRecoveryCode,
  postQuestion,
  readQuestions,
} from "../controllers/user.js";

const router = express.Router();

router.route("/signin").post(
  //multer("avatar", 512 * 1024),
  body("username").isLength({ min: 5 }),
  //body("username").isLength({ min: 5 }),
  //body("wallet").isNumeric(),
  signin
);
//add express validator for signUp
// show all users for the admin
router.route("/signup").post(signup);
//router.route("/forgotPassword").post(forgotPassword);
//router.route("/resetPassword/:token").post(resetPassword);
// New routes for password recovery
router.route("/sendRecoveryCode").post(sendRecoveryCode);
router.route("/verifyRecoveryCode").post(verifyRecoveryCode);
router.route("/updatePassword").post(updatePassword);
router.route("/postQuestion").post(postQuestion);
router.route("/readQuestions").get(readQuestions);

//router.put("/getUserCurrency", getUserCurrency);
// router
//   .route("/:id")
//   .put(
//     multer("avatar", 512 * 1024),
//     body("username").isLength({ min: 5 }),
//     body("username").isLength({ min: 5 }),
//     body("wallet").isNumeric(),
//     putOnce
//   );

export default router;
