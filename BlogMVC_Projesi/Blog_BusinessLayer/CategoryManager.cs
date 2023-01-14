using Blog_DataAccessLayer.EntityFrameworkSQL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_BusinessLayer
{
    public class CategoryManager : BaseManager<Category>
    {

        public override int Delete(Category category)
        {
            NoteManager noteManager = new NoteManager();
            CommentManager commentManager = new CommentManager();
            LikedManager likedManager = new LikedManager();
            foreach (var note in category.Notes.ToList())
            {
                foreach (var comment in note.Comments.ToList())
                {
                    commentManager.Delete(comment);
                }

                foreach (var like in note.Likes.ToList())
                {
                    likedManager.Delete(like);
                }
                noteManager.Delete(note);

            }



            return base.Delete(category);
        }
    }
}