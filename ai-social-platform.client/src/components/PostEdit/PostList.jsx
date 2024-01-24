import { useState, useEffect, useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useFormik } from 'formik';
import * as postService from '../../core/services/postService';
import * as mediaService from '../../core/services/mediaService';

import { CreateFormKeys, PATH } from '../../core/environments/costants';

import AuthContext from '../../contexts/authContext';

import Posts from '../Posts/Posts';
import styles from './Postedit.css';
//import Postedit from './components/PostEdit/Postedit';

export default function Postlist() {
    //const { firstName, userId } = useContext(AuthContext);
    //debugger;
    const [authUserData, setAuthUserData] = useState(null);
    const authContext = useContext(AuthContext);
    const [postData, setPostData] = useState(null);
    const [error, setError] = useState(null);
    useEffect(() => {
        // Използваме Promise.all за изчакване на завършването на двете заявки
        Promise.all([postService.getPostsByUserId(authContext.userId)])
            .then(([postResult]) => {
                setPostData(postResult);
            })
            .catch((error) => setError(error));
    }, []);
    console.log(postData);
    return (
        <div className="user-profile">
            <article className="post-item">
                <p>My Posts</p>
                {postData && postData.publications ? (
                    postData.publications.map((post) => (
                        <li className="userprofile-li" key={post.id}>
                            Post:{' '}
                            {post.content && post.content.length > 30
                                ? post.content.substring(0, 30) + '...'
                                : post.content}{' '}
                            Date Created: {post.dateCreated} Post id {post.id}{' '}
                            <Link to={`/posts/${post.id}`}>Read more Item</Link>{' '}
                            <Link
                                to={PATH.postedit.replace(':postId', post.id)}
                            >
                                Edit post
                            </Link>
                        </li>
                    ))
                ) : (
                    <p>Loading...</p>
                )}
            </article>
        </div>
    );
}
