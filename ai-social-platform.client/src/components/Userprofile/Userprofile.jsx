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
  const [friendsData, setFriendsData] = useState(null);
  const [error, setError] = useState(null);
  const authContext = useContext(AuthContext);
  let isUserFriend;

  useEffect(() => {
    // Използваме Promise.all за изчакване на завършването на двете заявки
    Promise.all([
      userService.getUserDetails(userId),
      userService.getUserDetails(authContext.userId),
      userService.getFriendsData(),
    ])
      .then(([userResult, authUserResult, friendsResult]) => {
        setUserData(userResult);
        setAuthUserData(authUserResult);
        setFriendsData(friendsResult);
      })
      .catch((error) => setError(error));
  }, [userId, authContext.userId, isUserFriend]);

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

  const isCurrentUserProfile = userId === authContext.userId;
  isUserFriend = friendsData.some((friend) => friend.id === userData.id);

  const handleAddFriend = async () => {
    try {
      await userService.addFriend(userId);
      // Промени състоянието, че сега потребителят е приятел
      isUserFriend = true;
      // Извикване на нова заявка, за да актуализира информацията за приятелите
      const friendsResult = await userService.getFriendsData();
      setFriendsData(friendsResult);
    } catch (error) {
      setError(error);
    }
  };

  const handleRemoveFriend = async () => {
    try {
      await userService.removeFriend(userId);
      // Промени състоянието, че сега потребителят не е приятел
      isUserFriend = false;
      // Извикване на нова заявка, за да актуализира информацията за приятелите
      const friendsResult = await userService.getFriendsData();
      setFriendsData(friendsResult);
      //console.log("remove", friendsData);
    } catch (error) {
      setError(error.message);
    }
  };

  console.log("userData", userData);
  console.log("authUserData", authUserData);
  console.log("friendsData", friendsData);
  console.log(userId, authContext.userId);
  console.log("isCurrentUserProfileis", isCurrentUserProfile);
  console.log("isUserFriend", isUserFriend);

  return (
    <div className="user-profile">
      <article className="post-item">
        <img
          className="user-cover"
          src={userData.coverPhotoUrl || "../../../public/images/Logo.png"}
          alt="User cover photo"
        />
        <div className="user-info-wrapper">
          <img
            className="user-img"
            src={
              userData.profilPictureUrl ||
              "../../../public/images/default-profile-pic.png"
            }
            alt="User profile pic"
          />

          <fieldset>
            <legend>Contact information</legend>
            <p className="username-profile">
              {userData.firstName} {userData.lastName}
            </p>

            <p className="posted-user">
              E-mail:
              <a href="mailto: {userData?.userName}"> {userData?.userName}</a>
            </p>
            <p className="posted-user">Phone Number: {userData.phoneNumber}</p>
          </fieldset>
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
                  <button
                    className="profile-button"
                    onClick={handleRemoveFriend}
                  >
                    Remove from Friends
                  </button>
                ) : (
                  <button className="profile-button" onClick={handleAddFriend}>
                    Add as Friend
                  </button>
                )}
              </>
            )}
          </div>
        </div>

        <fieldset>
          <legend>Personal data</legend>

          <p className="posted-user">Country: {userData.country}</p>
          <p className="posted-user">State: {userData.state}</p>
          <p className="posted-user">Gender: {userData.gender}</p>
          <p className="posted-user">School: {userData.school}</p>
          <p className="posted-user">Birthday: {formattedBirthday}</p>
          <p className="posted-user">
            Relationship Status: {userData.relationship}
          </p>
        </fieldset>
        <fieldset>
          <legend>Friends</legend>
          <div>
            <ul>
              {friendsData.map((friend) => (
                <li key={friend.id}>
                  <Link to={PATH.userProfile(friend.id)}>
                    <img
                      className="friend-img"
                      src={
                        friend.profilPictureUr ||
                        "../../../public/images/default-profile-pic.png"
                      }
                      alt="User profile pic"
                    />{" "}
                    {friend.firstName} {friend.lastName}
                  </Link>
                </li>
              ))}
            </ul>
          </div>
        </fieldset>
      </article>
    </div>
  );
}
