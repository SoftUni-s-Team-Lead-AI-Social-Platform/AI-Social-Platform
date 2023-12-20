import { useEffect, useState } from "react";
import styles from "./Userprofile.css";
import * as userService from "../../core/services/userService";
import { Link, useNavigate, useParams } from "react-router-dom";
import { useFormik, Field } from "formik";
import { PATH, ProfileFormKeys } from "../../core/environments/costants";
import userProfileValidation from "./userProfileValidation";

export default function Userprofileedit() {
  const { userId } = useParams();
  const [userData, setUserData] = useState({});
  const [error, setError] = useState(null);

  useEffect(() => {
    userService
      .getUserData(userId)
      .then((result) => {
        console.log(result);
        setUserData(result);
      })
      .catch((error) => console.log(error));
  }, []);
  if (!userData) {
    return <div>Loading...</div>;
  }
  console.log(userData.firstName);
  const initialValues = {
    [ProfileFormKeys.FirstName]: userData.firstName,
    [ProfileFormKeys.LastName]: userData.lastName,
    [ProfileFormKeys.PhoneNumber]: userData.phoneNumber,
    [ProfileFormKeys.ProfilePicture]: userData.profilePictureBase64,
    [ProfileFormKeys.CoverPhoto]: userData.coverPhotoBase64,
    [ProfileFormKeys.Country]: userData.country,
    [ProfileFormKeys.State]: userData.state,
    [ProfileFormKeys.Gender]: userData.gender,
    [ProfileFormKeys.School]: userData.school,
    [ProfileFormKeys.Birthday]: userData.birthday,
    [ProfileFormKeys.Relationship]: userData.relationship,
  };

  const {
    values,
    errors,
    isSubmitting,
    touched,
    handleChange,
    handleBlur,
    handleSubmit,
  } = useFormik({
    initialValues,
    enableReinitialize: true,
    onSubmit,
    validationSchema: userProfileValidation,
  });

  const updateSubmitHandler = async ({
    firstName,
    lastName,
    phoneNumber,
    profilePicture,
    coverPhoto,
    country,
    state,
    gender,
    school,
    birthday,
    relationship,
    schools,
  }) => {
    await userService.update({
      firstName,
      lastName,
      phoneNumber,
      profilePicture,
      coverPhoto,
      country,
      state,
      gender,
      school,
      birthday,
      relationship,
      schools,
    });

    // setAuth(result);

    // localStorage.setItem('accessToken', result.token);

    useNavigate(PATH.home);
  };

  async function onSubmit(values) {
    debugger;
    try {
      await updateSubmitHandler(values);
    } catch (error) {
      console.log("Error:", error);
    }
  }
  return (
    <div className="user-profile">
      <article className="post-item">
        <form onSubmit={handleSubmit}>
          <img
            className="user-cover"
            src={
              values[ProfileFormKeys.CoverPhoto] ||
              "../../../public/images/Logo.png"
            }
            alt="User cover photo"
          />
          <div className="user-info-wrapper">
            <img
              className="user-img"
              src="../../../public/images/mamut.jpg"
              alt="User profile pic"
            />
            <div className="user-info-text">
              <div className="username-profile">
                <label htmlFor={ProfileFormKeys.ProfilePicture}>
                  Change the ProfilePicture:
                </label>

                <input
                  type="text"
                  id={ProfileFormKeys.ProfilePicture}
                  name={ProfileFormKeys.ProfilePicture}
                  placeholder="Upload a photo..."
                  onChange={handleChange}
                  onBlur={handleBlur}
                  value={values[ProfileFormKeys.ProfilePicture]}
                />
                <div>
                  <label htmlFor={ProfileFormKeys.CoverPhoto}>
                    Change the CoverPhoto:
                  </label>

                  <input
                    type="text"
                    id={ProfileFormKeys.CoverPhoto}
                    name={ProfileFormKeys.CoverPhoto}
                    placeholder="Upload a photo..."
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values[ProfileFormKeys.CoverPhoto]}
                  />
                </div>
                <label htmlFor={ProfileFormKeys.FirstName}>First Name </label>
                <input
                  className={
                    errors[ProfileFormKeys.FirstName] &&
                    touched[ProfileFormKeys.FirstName]
                      ? styles["name-input-error"]
                      : styles["name-input"]
                  }
                  type="text"
                  name={ProfileFormKeys.FirstName}
                  id={ProfileFormKeys.FirstName}
                  placeholder="First name"
                  onChange={handleChange}
                  onBlur={handleBlur}
                  value={values[ProfileFormKeys.FirstName]}
                />

                {errors[ProfileFormKeys.FirstName] &&
                  touched[ProfileFormKeys.FirstName] && (
                    <p className={styles["error-message"]}>
                      {errors[RegisterFormKeys.FirstName]}
                    </p>
                  )}

                <div className={styles["last-name"]}>
                  <label htmlFor={ProfileFormKeys.LastName}>Last Name </label>
                  <input
                    className={
                      errors[ProfileFormKeys.LastName] &&
                      touched[ProfileFormKeys.LastName]
                        ? styles["name-input-error"]
                        : styles["name-input"]
                    }
                    type="text"
                    name={ProfileFormKeys.LastName}
                    id={ProfileFormKeys.LastName}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values[ProfileFormKeys.LastName]}
                  />

                  {errors[ProfileFormKeys.LastName] &&
                    touched[ProfileFormKeys.LastName] && (
                      <p className={styles["error-message"]}>
                        {errors[ProfileFormKeys.LastName]}
                      </p>
                    )}
                </div>

                <section className={styles["phone-number-wrapper"]}>
                  <label htmlFor={ProfileFormKeys.PhoneNumber}>
                    Phone Number
                  </label>
                  <input
                    className={
                      errors[ProfileFormKeys.PhoneNumber] &&
                      touched[ProfileFormKeys.PhoneNumber]
                        ? styles["input-field-error"]
                        : styles["input-field"]
                    }
                    type="text"
                    name={ProfileFormKeys.PhoneNumber}
                    id={ProfileFormKeys.PhoneNumber}
                    placeholder="Phone number"
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values[ProfileFormKeys.PhoneNumber]}
                  />

                  {errors[ProfileFormKeys.PhoneNumber] &&
                    touched[ProfileFormKeys.PhoneNumber] && (
                      <p className={styles["error-message"]}>
                        {errors[ProfileFormKeys.PhoneNumber]}
                      </p>
                    )}
                </section>

                <section className={styles["phone-number-wrapper"]}>
                  <label htmlFor={ProfileFormKeys.Country}>Country </label>
                  <input
                    className={
                      errors[ProfileFormKeys.Country] &&
                      touched[ProfileFormKeys.Country]
                        ? styles["input-field-error"]
                        : styles["input-field"]
                    }
                    type="text"
                    name={ProfileFormKeys.Country}
                    id={ProfileFormKeys.Country}
                    placeholder="Country"
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values[ProfileFormKeys.Country]}
                  />

                  {errors[ProfileFormKeys.Country] &&
                    touched[ProfileFormKeys.Country] && (
                      <p className={styles["error-message"]}>
                        {errors[ProfileFormKeys.Country]}
                      </p>
                    )}
                </section>

                <section className={styles["phone-number-wrapper"]}>
                  <label htmlFor={ProfileFormKeys.State}>State </label>
                  <input
                    className={
                      errors[ProfileFormKeys.State] &&
                      touched[ProfileFormKeys.State]
                        ? styles["input-field-error"]
                        : styles["input-field"]
                    }
                    type="text"
                    name={ProfileFormKeys.State}
                    id={ProfileFormKeys.State}
                    placeholder="State"
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values[ProfileFormKeys.State]}
                  />

                  {errors[ProfileFormKeys.State] &&
                    touched[ProfileFormKeys.State] && (
                      <p className={styles["error-message"]}>
                        {errors[ProfileFormKeys.State]}
                      </p>
                    )}
                </section>

                <section className={styles["phone-number-wrapper"]}>
                  <label htmlFor={ProfileFormKeys.Gender}>Gender </label>
                  <label>
                    <input
                      type="radio"
                      name={ProfileFormKeys.Gender}
                      id={ProfileFormKeys.Gender}
                      value="Man"
                      checked={values[ProfileFormKeys.Gender] === "Man"}
                      onChange={handleChange}
                      onBlur={handleBlur}
                    />
                    Man
                  </label>
                  <label>
                    <input
                      type="radio"
                      name={ProfileFormKeys.Gender}
                      id={ProfileFormKeys.Gender}
                      value="Woman"
                      checked={values[ProfileFormKeys.Gender] === "Woman"}
                      onChange={handleChange}
                      onBlur={handleBlur}
                    />
                    Woman
                  </label>
                </section>

                <section className={styles["phone-number-wrapper"]}>
                  <label htmlFor={ProfileFormKeys.School}>School </label>
                  <input
                    className={
                      errors[ProfileFormKeys.School] &&
                      touched[ProfileFormKeys.School]
                        ? styles["input-field-error"]
                        : styles["input-field"]
                    }
                    type="text"
                    name={ProfileFormKeys.School}
                    id={ProfileFormKeys.School}
                    placeholder="School"
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values[ProfileFormKeys.School]}
                  />

                  {errors[ProfileFormKeys.School] &&
                    touched[ProfileFormKeys.School] && (
                      <p className={styles["error-message"]}>
                        {errors[ProfileFormKeys.School]}
                      </p>
                    )}
                </section>

                <section className={styles["phone-number-wrapper"]}>
                  <label htmlFor={ProfileFormKeys.Birthday}>Birthday </label>
                  <input
                    className={
                      errors[ProfileFormKeys.Birthday] &&
                      touched[ProfileFormKeys.Birthday]
                        ? styles["input-field-error"]
                        : styles["input-field"]
                    }
                    type="text"
                    name={ProfileFormKeys.Birthday}
                    id={ProfileFormKeys.Birthday}
                    placeholder="Birthday"
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values[ProfileFormKeys.Birthday]}
                  />

                  {errors[ProfileFormKeys.Birthday] &&
                    touched[ProfileFormKeys.Birthday] && (
                      <p className={styles["error-message"]}>
                        {errors[ProfileFormKeys.Birthday]}
                      </p>
                    )}
                </section>

                <section className={styles["phone-number-wrapper"]}>
                  <label htmlFor={ProfileFormKeys.Relationship}>
                    Relationship
                  </label>
                  <label>
                    <input
                      type="radio"
                      name={ProfileFormKeys.Relationship}
                      id={ProfileFormKeys.Relationship}
                      value="0"
                      checked={values[ProfileFormKeys.Relationship] === 0}
                      onChange={handleChange}
                      onBlur={handleBlur}
                    />
                    0
                  </label>
                  <label>
                    <input
                      type="radio"
                      name={ProfileFormKeys.Relationship}
                      id={ProfileFormKeys.Relationship}
                      value="1"
                      checked={values[ProfileFormKeys.Relationship] === 1}
                      onChange={handleChange}
                      onBlur={handleBlur}
                    />
                    1
                  </label>
                  <label>
                    <input
                      type="radio"
                      name={ProfileFormKeys.Relationship}
                      id={ProfileFormKeys.Relationship}
                      value="2"
                      checked={values[ProfileFormKeys.Relationship] === 2}
                      onChange={handleChange}
                      onBlur={handleBlur}
                    />
                    2
                  </label>
                </section>
              </div>
            </div>
          </div>

          <button
            type="submit"
            className={styles["register-button"]}
            disabled={isSubmitting}
          >
            Update
          </button>
        </form>
      </article>
    </div>
  );
}
