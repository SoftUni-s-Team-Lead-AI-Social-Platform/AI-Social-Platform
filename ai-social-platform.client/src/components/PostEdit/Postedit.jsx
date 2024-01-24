import { useContext, useEffect, useState } from 'react';
import styles from './Postedit.css';
import * as postService from '../../core/services/postService';
import * as mediaService from '../../core/services/mediaService';
import { Link, useNavigate, useParams } from 'react-router-dom';
import { useFormik, Field } from 'formik';
import { EditPostFormKeys, PATH } from '../../core/environments/costants';
//import Posts from '../Posts/Posts';

export default function Postedit() {
    const { postId } = useParams();
    const [postData, setPostData] = useState({});
    const [mediaData, setMediaData] = useState([]);
    const [error, setError] = useState(null);
    const [textareaRows, setTextareaRows] = useState(2);
    useEffect(() => {
        Promise.all([
            postService.getPostById(postId),
            mediaService.getMediaByPostId(postId),
        ])
            .then(([postResult, mediaResult]) => {
                setPostData(postResult);
                setMediaData(mediaResult);
            })
            .catch((error) => setError(error));
    }, []);

    if (!postData) {
        return <div>Loading...</div>;
    }

    // useEffect(() => {
    //     mediaService
    //         .getMediaByPostId(postId)
    //         .then((mediaResult) => {
    //             setMediaData(mediaResult);
    //         })
    //         .catch((error) => setError(error));
    // }, []);
    //console.log(mediaData[0].url);

    const initialValues = {
        [EditPostFormKeys.PostDescription]: postData.content,
        [EditPostFormKeys.TopicId]: postData.topic,
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
    });
    const incrementTextareaRows = () => setTextareaRows(7);
    async function onSubmit(values) {
        // const formData = new FormData();
        // formData.append('content', values.PostDescription);
        // formData.append('topicId', values.TopicId);

        // try {
        //     await postService.editPost(formData);
        // } catch (error) {
        //     console.log('Error:', error);
        // }
        const requestBody = {
            content: values[EditPostFormKeys.PostDescription],
            topicId: values[EditPostFormKeys.TopicId],
        };
        console.log(requestBody);
        try {
            await postService.editPost(postId, requestBody);
        } catch (error) {
            console.log('Error:', error);
        }
    }

    return (
        <div className="user-profile">
            <article className="post-item">
                <h2 className="section-heading">Edit Post</h2>
                <form className="create-form" onSubmit={handleSubmit}>
                    <textarea
                        className="post-description"
                        name={EditPostFormKeys.PostDescription}
                        id={EditPostFormKeys.PostDescription}
                        onFocus={incrementTextareaRows}
                        rows={textareaRows}
                        onChange={handleChange}
                        value={values[EditPostFormKeys.PostDescription]}
                    ></textarea>
                    {mediaData.map((media) => (
                        <li className="userprofile-li" key={media.fileId}>
                            <img
                                className="media-img"
                                src={media.url}
                                alt="Post pic"
                            />
                        </li>
                    ))}
                    <div className="parent-button">
                        <button
                            className="profile-button"
                            type="submit"
                            disabled={isSubmitting}
                        >
                            Submit
                        </button>
                    </div>
                </form>
            </article>
        </div>
    );
}
