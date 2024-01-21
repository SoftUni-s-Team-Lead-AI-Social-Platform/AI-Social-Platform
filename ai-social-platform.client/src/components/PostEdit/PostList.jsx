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
    const [postData, setPostData] = useState(null);
    const [error, setError] = useState(null);
    useEffect(() => {
        // Използваме Promise.all за изчакване на завършването на двете заявки
        Promise.all([postService.getAllPosts(1)])
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
                            <Link
                                to={
                                    '/posts/67a97399-04b0-41c8-90a7-17bc238a3c36'
                                }
                            >
                                Read more Item
                            </Link>{' '}
                            <Link
                                to={PATH.postedit.replace(
                                    ':postId',
                                    '67a97399-04b0-41c8-90a7-17bc238a3c36'
                                )}
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
