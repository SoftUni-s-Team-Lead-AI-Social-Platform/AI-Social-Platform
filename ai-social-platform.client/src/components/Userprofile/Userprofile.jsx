import { useState, useEffect, useContext } from "react";
import styles from "./Userprofile.css";
import * as userService from "../../core/services/userService";
import { Link, useParams } from "react-router-dom";
import { PATH } from "../../core/environments/costants";
import { pathToUrl } from "../Userprofile/pathUtils";
import AuthContext from "../../contexts/authContext";

export default function Userprofile() {
  const { userId } = useParams();

  const [userData, setUserData] = useState(null);
  const [authUserData, setAuthUserData] = useState(null);
  const [error, setError] = useState(null);
  const authContext = useContext(AuthContext);

  useEffect(() => {
    userService
      .getUserDetails(userId)
      .then((result) => {
        setUserData(result);
      })
      .catch((error) => setError(error));
    userService
      .getUserDetails(authContext.userId)
      .then((result) => {
        setAuthUserData(result);
      })
      .catch((error) => setError(error));
  }, [userId, authContext.userId]);

  if (error) {
    return <div>{error}</div>;
  }

  if (!userData) {
    return <div>Loading...</div>;
  }

  const date = new Date(userData.birthday);
  const day = date.getDate();
  const month = date.getMonth() + 1;
  const year = date.getFullYear();
  const formattedBirthday = `${day < 10 ? "0" : ""}${day}/${
    month < 10 ? "0" : ""
  }${month}/${year}`;

  console.log("userData", userData);
  console.log("authUserData", authUserData);
  let isCurrentUserProfile = userId === authContext.userId;
  let isUserFriend = true;
  if (isCurrentUserProfile) {
    isUserFriend = true;
  } else {
    isUserFriend = authUserData.friends.some(
      (friend) => friend.id === userData.userId
    );
  }
  isCurrentUserProfile = false;
  isUserFriend = false;
  console.log("isUserFriend", isUserFriend);
  const handleAddFriend = async () => {
    try {
      await userService.addFriend(userId);
      // Промени състоянието, че сега потребителят е приятел
      // setIsFriend(true);
      // Извикване на нова заявка, за да актуализира информацията за приятелите
      // const authUserResult = await userService.getUserDetails(authContext.userId);
      // setAuthUserData(authUserResult);
    } catch (error) {
      setError(error);
    }
  };

  const handleRemoveFriend = async () => {};
  //const isCurrentUserProfile = false;

  console.log("userData", userData);
  console.log("authUserData", authUserData);
  //console.log("authContext", authContext);
  console.log(userId, authContext.userId);
  console.log(isCurrentUserProfile);

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
            <div>
              <p className="posted-user">Friends</p>
              <ul>
                {userData.friends.map((friend) => (
                  <li key={friend.id}>
                    <Link to={PATH.userProfile(friend.id)}>
                      {friend.firstName} {friend.lastName}
                    </Link>
                  </li>
                ))}
              </ul>
            </div>
          </div>

          <div className="edit">
            {isCurrentUserProfile && (
              <Link to={pathToUrl(PATH.profileedit, { userId: userId })}>
                <img
                  className="edit-img"
                  src="../../../public/images/edit-pen-icon-6.jpg"
                  alt="edit button"
                />
              </Link>
            )}
            {!isCurrentUserProfile && (
              <>
                {isUserFriend ? (
                  <button onClick={handleRemoveFriend}>
                    Remove from Friends
                  </button>
                ) : (
                  <button onClick={handleAddFriend}>Add as Friend</button>
                )}
              </>
            )}
          </div>
        </div>
      </article>
    </div>
  );
}
