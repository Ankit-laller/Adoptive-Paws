using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Core.Queries
{
    public class AdoptionAppServiceQueries
    {
        public static readonly string deleteImagesQuery = "delete from Images WHERE PetId = @PetId";
        public static readonly string deletePetQuery = "delete from Pets WHERE PetId = @PetId";
        public static readonly string checkQuery = "select * from Pets where petId =@Id";
        public static readonly string insertImageQuery = @"insert into Images ( ImageUrl, ImageName,CreatedOn, petId) values
            ( @ImageUrl,@ImageName,@CreatedOn,@petId)";
        public static readonly string insertPetDataQuery = @"insert into Pets (petId, petName,description,petAge,vaccinated ,
            petGender,petType,userId, isAdopted, price,address,createdOn) values(@PetId,@PetName,@Description,@PetAge,@Vaccinated,
            @PetGender,@PetType,@UserId, @IsAdopted,
            @Price,@Address,@CreatedOn)";

        public static readonly string sendAdoptionRequestQuery = @"insert into AdoptionRequests (requestId,
        senderId, senderName, ownerId, petId,petName) values(@RequestId, @SenderId,@SenderName,@OwnerId,@PetId,@PetName)";
        public static readonly string deleteAdoptionRequestQuery = @"delete from AdoptionRequests where requestId= @Id";
        public static readonly string acceptAdoptionRequestQuery = @"update AdoptionRequests set status=1 where requestId= @Id";
        public static readonly string adoptionQuery = @"update Pets set isAdopted = 1 where petId = @Id";
        public static readonly string checkRequest = @"select * from AdoptionRequests where senderId=@SenderId and petId= @PetId and status=0";


        public static readonly string fetchAdoptionRequestQuery = @"select * from  AdoptionRequests where ownerId= @Id and status=0";

        public static readonly string CheckDataInTable = @"select * from @TableName where @ColumnName=@ColumnValue order by 1 desc";
    }
}
