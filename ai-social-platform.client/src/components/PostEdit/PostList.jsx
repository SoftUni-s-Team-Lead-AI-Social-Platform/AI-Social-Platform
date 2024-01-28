import { useState, useEffect, useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useFormik } from 'formik';
import * as postService from '../../core/services/postService';
import * as mediaService from '../../core/services/mediaService';

import { CreateFormKeys, PATH, host } from '../../core/environments/costants';

import AuthContext from '../../contexts/authContext';

import Posts from '../Posts/Posts';
import styles from './Postedit.css';
import dateFormater from '../../utils/dateFormatter';
//import Postedit from './components/PostEdit/Postedit';

export default function Postlist() {
    //const { firstName, userId } = useContext(AuthContext);
    //debugger;
    const [authUserData, setAuthUserData] = useState(null);
    const authContext = useContext(AuthContext);
    const [postData, setPostData] = useState(null);
    const [error, setError] = useState(null);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);

    useEffect(() => {
        Promise.all([
            postService.getPostsByUserId(authContext.userId, currentPage),
        ])
            .then(([postResult]) => {
                setPostData(postResult);
                setTotalPages(postResult.totalPages);
            })
            .catch((error) => setError(error));
    }, [currentPage]);

    //console.log(postData);
    // const handleRemovePost = async (id) => {
    //     //debugger;
    //     try {
    //         await postService.deletePost(id);
    //     } catch (error) {
    //         setError(error.message);
    //     }
    // };
    const handleRemovePost = async (id) => {
        const shouldDelete = window.confirm(
            'Are you sure you want to delete this post?'
        );
        if (shouldDelete) {
            try {
                await postService.deletePost(id);
                reloadPostList(); // Извикване на функцията за презареждане след успешно изтриване
            } catch (error) {
                setError(error.message);
            }
        }
    };

    const reloadPostList = () => {
        window.location.href = PATH.postlist;
    };
    const goToNextPage = () => {
        if (currentPage < totalPages) {
            setCurrentPage(currentPage + 1);
        }
    };

    const goToPrevPage = () => {
        if (currentPage > 1) {
            setCurrentPage(currentPage - 1);
        }
    };

    return (
        <div className="user-profile">
            <article className="post-item">
                <p className="section-heading">My Posts</p>
                {postData && postData.publications ? (
                    <>
                        {postData.publications.map((post) => (
                            // <li className="userprofile-li" key={post.id}>
                            //     Post:{' '}
                            //     {post.content && post.content.length > 30
                            //         ? post.content.substring(0, 30) + '...'
                            //         : post.content}{' '}
                            //     Date Created: {post.dateCreated} Post id {post.id}{' '}
                            //     <Link to={`/posts/${post.id}`}>Read more Item</Link>{' '}
                            //     <Link
                            //         to={PATH.postedit.replace(':postId', post.id)}
                            //     >
                            //         Edit post
                            //     </Link>
                            //     <button
                            //         className="profile-button"
                            //         onClick={() => handleRemovePost(post.id)}
                            //     >
                            //         Remove Post
                            //     </button>
                            // </li>
                            <article className="post-item">
                                <section
                                    // ref={mediaSectionRef}
                                    className="content-description"
                                >
                                    <p>
                                        {post.content &&
                                        post.content.length > 30
                                            ? post.content.substring(0, 30) +
                                              '...'
                                            : post.content}{' '}
                                    </p>
                                    <div className="posted-on">
                                        <p>
                                            <b>Posted on:</b>{' '}
                                            {dateFormater(post?.dateCreated)}{' '}
                                        </p>
                                        <p>
                                            <b>Latest activity:</b>{' '}
                                            {dateFormater(post?.latestActivity)}
                                        </p>
                                    </div>
                                    <section className="likes">
                                        <div className="likes-count">
                                            <i className="fa-solid fa-thumbs-up"></i>
                                            <p>{post.likesCount}</p>
                                            {'  '}{' '}
                                        </div>
                                        <p className="comments">
                                            {post.commentsCount}
                                            {'  '}
                                            comments
                                        </p>
                                    </section>
                                </section>

                                <div className="userprofile-li">
                                    <Link
                                        to={`/posts/${post.id}`}
                                        // clasName="userprofile-li"
                                    >
                                        Read more Item
                                    </Link>{' '}
                                    <Link
                                        to={PATH.postedit.replace(
                                            ':postId',
                                            post.id
                                        )}
                                        // clasName="userprofile-li"
                                    >
                                        Edit post
                                    </Link>{' '}
                                    <button
                                        // className="profile-button"
                                        onClick={() =>
                                            handleRemovePost(post.id)
                                        }
                                    >
                                        Remove Post
                                    </button>
                                </div>
                            </article>
                        ))}
                        <div className="pagination">
                            <button
                                onClick={goToPrevPage}
                                disabled={currentPage === 1}
                            >
                                Prev{' '}
                            </button>
                            <span> {`${currentPage} / ${totalPages}`} </span>
                            <button
                                onClick={goToNextPage}
                                disabled={currentPage === totalPages}
                            >
                                Next
                            </button>
                        </div>
                    </>
                ) : (
                    <p>Loading...</p>
                )}
            </article>
        </div>
    );
}
