import { AxiosInstance } from "axios";
import { Blog } from "./modules/Blog";

const BLOGS_PUBLIC_URL = `/public/api/blogs`;
const BLOGS_URL = `/api/blogs`;

const Blogs = {
  loadBlogsByPage: (client: AxiosInstance, pageNumber: number) =>
    client.get(`${BLOGS_PUBLIC_URL}?page=${pageNumber + 1}`),
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
