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
    const [mediaData, setMediaData] = useState([]);

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
                const [mediaResult] = await Promise.all([
                    mediaService.getMediaByPostId(id),
                ]);
                const mediaData = mediaResult || [];

                console.log('mediaData', mediaData);

                if (mediaData.length > 0) {
                    // Събираме всички Promise-и за изтриване на снимките в един масив
                    const deletePromises = mediaData.map(async (media) => {
                        await mediaService.deleteMedia(media.fileId);
                    });

                    // Изчакваме всички Promise-и да завършат
                    await Promise.all(deletePromises);

                    // Изтриването на снимките е успешно
                    console.log('Всички снимки бяха изтрити успешно');
                }

                // Сега можем да изтрием поста
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
            <article className="post-item-list">
                <p className="section-heading">My Posts</p>
                {postData && postData.publications ? (
                    <>
                        {postData.publications.map((post) => (
                            <article className="post-item-list">
                                <section
                                    // ref={mediaSectionRef}
                                    className="content-description"
                                >
                                    <p>
                                        {post.content &&
                                        post.content.length > 80
                                            ? post.content.substring(0, 80) +
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
