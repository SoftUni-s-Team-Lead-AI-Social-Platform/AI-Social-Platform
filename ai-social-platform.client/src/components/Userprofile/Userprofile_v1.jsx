import {
  useContext,
  useEffect,
  useReducer,
  useState,
  useMemo,
  useRef,
} from "react";
import { Link, useNavigate, useParams, useLocation } from "react-router-dom";

import styles from "./Userprofile.css";

export default function Userprofile() {
  const inputRef = useRef(null);
  const mediaSectionRef = useRef(null);
  const userAll = useParams().userID;

  const focusInput = () => {
    if (inputRef.current && mediaSectionRef.current) {
      mediaSectionRef.current.scrollIntoView();
      inputRef.current.focus();
    }
  };

  const { userID } = useParams();
  const [userData, setUserData] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(
          `https://localhost:5173/api/User/userDetail/${userID}`
        );
        const data = await response.json();
        console.log(data);

        console.log(userData);
      } catch (error) {
        console.error("Error fetching user data: ", error);
      }
    };

    fetchData();
  }, [userID]);

  if (!userData) {
    return <p>Loading...</p>;
  }

  {
    /*const [userData, setUserData] = useState({
      FirstName: "Mamuta",
      LastName: "Mani",
      Email: "test@gmail.com",
      CountryId: 1,
      StateId: 1,
      Birthday: "5.11.2007 г. 00:00:00 ч.",
      Relationship: null,
      Gender: 1,
    });*/
  }

  {
    /*useEffect(() => {
      if (userData.CoverPhoto) {
        const coverPhotoBlob = new Blob([userData.CoverPhoto], {
          type: "image/*",
        });
        const coverPhotoDataUrl = URL.createObjectURL(coverPhotoBlob);
        if (userData.CoverPhoto !== coverPhotoDataUrl) {
          setUserData((prevData) => ({
            ...prevData,
            CoverPhoto: coverPhotoDataUrl,
          }));
        }
      }
  
      {
        /*if (userData.ProfilePicture) {
        const profilePictureBlob = new Blob([userData.ProfilePicture], {
          type: "image/*",
        });
        const profilePictureDataUrl = URL.createObjectURL(profilePictureBlob);
  
        setUserData((prevData) => ({
          ...prevData,
          ProfilePicture: profilePictureDataUrl,
        }));
      }
    }, [userData.FirstName]);*/
  }

  return (
    <div className="user-profile">
      <article className="post-item">
        <img
          className="user-cover"
          src="../../../public/images/pexels-aviv-perets-3274903.jpg"
          alt="User cover photo"
        />
        <div className="user-info-wrapper">
          <img
            className="user-img"
            src="../../../public/images/mamut.jpg"
            alt="User profile pic"
          />
          <div className="user-info-text">
            <p className="username-profile">
              {userData.FirstName} {userData.LastName}
            </p>
            <p className="posted-user">
              E-mail:
              <a href="mailto: {userData.Email}"> {userData.Email}</a>
            </p>
          </div>

          <div className="edit">
            <a href="#">
              <img
                className="edit-img"
                src="../../../public/images/edit-pen-icon-6.jpg"
                alt="edit button"
              />
            </a>
            <a href="#">
              <img
                className="bin-img"
                src="../../../public/images/icon-remove-22.jpg"
                alt="bin button"
              />
            </a>
          </div>
        </div>
        <section className="content-description">
          <p>
            City, Country: {userData.StateId}, {userData.CountryId}
          </p>
          <p>Gender: {userData.Gender}</p>
          <p>Birthday: {userData.Birthday}</p>
          <p>Relationship Status:{userData.Relationship}</p>

          <p>
            <a href="#">Publications</a>
          </p>
          <p>
            <a href="#">Friends</a>
          </p>
          <p>{userAll}</p>
        </section>
      </article>
    </div>
  );
}
