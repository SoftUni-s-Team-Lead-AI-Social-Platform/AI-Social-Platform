import { ContentType, endpoints } from '../environments/costants';
import * as api from './api';

export const getLoggedUserDetails = async () => {
    const result = await api.get(endpoints.getLoggedUserDetails);

    return result;
};

export const getUserData = async (userId) => {
    const result = await api.get(`${endpoints.userDetails}/${userId}`);

    return result;
};

export const update = async (values) =>
    await api.put(endpoints.updateUser, values, ContentType.ApplicationJSON);

export const getUserDetails = async (userId) =>
    await api.get(endpoints.userDetails(userId));

export const addFriend = async (userId) =>
    await api.post(endpoints.addFriend(userId));
