import { useState, useEffect } from "react";
import styles from "./Userprofile.css";
import * as userService from "../../core/services/userService";
import { Link, useParams } from "react-router-dom";
import { PATH } from "../../core/environments/costants";
import { pathToUrl } from "../Userprofile/pathUtils";

export default function Userprofile() {
  const { userId } = useParams();

  const [userData, setUserData] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    userService
      .getUserDetails(userId)
      .then((result) => {
        setUserData(result);
      })
      .catch((error) => setError(error));
  }, []);

  if (error) {
    return <div>{error}</div>;
  }

  if (!userData) {
    return <div>Loading...</div>;
  }

  const date = new Date(userData.birthday);

  const day = date.getDate();
  const month = date.getMonth() + 1; // Тъй като месеците са от 0 до 11
  const year = date.getFullYear();

  const formattedBirthday = `${day < 10 ? "0" : ""}${day}/${
    month < 10 ? "0" : ""
  }${month}/${year}`;

  return (
    <div className="user-profile">
      <article className="post-item">
        <img
          className="user-cover"
          src="../../../public/images/iceage.png"
          alt="User cover photo"
        />
        <div className="user-info-wrapper">
          <img
            className="user-img"
            src="../../../public/images/mamut.jpg"
            alt="User profile pic"
          />
          <div className="user-info-text">
            <p className="cover-profile">User Profile</p>

            <p className="username-profile">
              {userData.firstName} {userData.lastName}
            </p>
            <p className="posted-user">
              E-mail:
              <a href="mailto: {userData?.userName}"> {userData?.userName}</a>
            </p>
            <p className="posted-user">GSM: {userData.phoneNumber}</p>
            <p className="posted-user">Country: {userData.country}</p>
            <p className="posted-user">State: {userData.state}</p>
            <p className="posted-user">Gender: {userData.gender}</p>
            <p className="posted-user">School: {userData.school}</p>
            <p className="posted-user">Birthday: {formattedBirthday}</p>
            <p className="posted-user">
              Relationship Status: {userData.relationship}
            </p>
            <p className="posted-user">
              <a href="#">Friends</a>
            </p>
          </div>

          <div className="edit">
            <Link to={pathToUrl(PATH.profileedit, { userId: userId })}>
              <img
                className="edit-img"
                src="../../../public/images/edit-pen-icon-6.jpg"
                alt="edit button"
              />
            </Link>
          </div>
        </div>
      </article>
    </div>
  );
}
