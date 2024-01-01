import { endpoints } from '../environments/costants';
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
    await api.put(endpoints.updateUser, values);

export const getUserDetails = async (userId) =>
    await api.get(endpoints.userDetails(userId));

export const addFriend = async (userId) =>
    await api.post(endpoints.addFriend(userId));

export const removeFriend = async (userId) =>
    await api.post(endpoints.removeFriend(userId));

export const getFriendsData = async () =>
    await api.get(endpoints.allFriends);
    

