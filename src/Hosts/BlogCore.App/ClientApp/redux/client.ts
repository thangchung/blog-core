import axios, { AxiosInstance } from "axios";
import { globalConfig as GlobalConfig } from "./../configs";
import { Blog } from "./modules/Blog";

const BLOGS_PUBLIC_URL = `/public/api/blogs`;
const BLOGS_URL = `/api/blogs`;

// TODO: will refactor this later
export const client: AxiosInstance = axios.create({
  baseURL: GlobalConfig.apiServer,
  timeout: 2000
});

const Blogs = {
  loadBlogsByPage: (client: AxiosInstance, pageNumber: number) =>
    client.get(`${BLOGS_PUBLIC_URL}?page=${pageNumber + 1}`),
  loadBlogById: (client: AxiosInstance, id: string) =>
    client.get(`${BLOGS_PUBLIC_URL}/${id}`),
  createBlog: (client: AxiosInstance, blog: Blog) =>
    client.post(BLOGS_URL, blog),
  editBlog: (client: AxiosInstance, blog: Blog) =>
    client.put(`${BLOGS_URL}/${blog.id}`, blog),
  deleteBlog: (client: AxiosInstance, blogId: string) =>
    client.delete(`${BLOGS_URL}/${blogId}`)
};

export default {
  Blogs
};
