using ResolutionManagement.Data;
using Microsoft.EntityFrameworkCore;
using ResolutionManagement.Models;
using System.Linq;

namespace ResolutionManagementSystem.Data
{
    public static class ResolutionSeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resolution>().HasData(
                GetResolutions()
            );
            modelBuilder.Entity<FeedbackRequest>().HasData(
                GetFeedbackRequests()
            );
        }
        public static List<Resolution> GetResolutions()
        {
            // Owner IDS in Order: 2, 2, 1, 5
            List<Resolution> resolutions = new List<Resolution>() {
            new Resolution() {
                ResolutionId=0,
                CreationDate=DateTime.Now,
                Abstract="",
                Status="in progress",
                OwnerUserID="221fedc9-3ad4-492e-bfc0-20f198923a24"
            },
            new Resolution() {
                ResolutionId=1,
                CreationDate=DateTime.Now,
                Abstract="",
                Status="in progress",
                OwnerUserID="221fedc9-3ad4-492e-bfc0-20f198923a24"
            },
            new Resolution() {
                ResolutionId=2,
                CreationDate=DateTime.Now,
                Abstract="",
                Status="in progress",
                OwnerUserID="37c1ba03-d67c-437e-ac19-2b38b123c55a"
            },
            new Resolution() {
                ResolutionId=3,
                CreationDate=DateTime.Now,
                Abstract="",
                Status="in progress",
                OwnerUserID="5559d343-5062-4cd1-b0ae-25301e70a10d"
            },
        };
            return resolutions;
        }

        public static List<FeedbackRequest> GetFeedbackRequests()
        {
            // Owner IDS in Order: 2, 2, 1, 5
            List<FeedbackRequest> feedbackRequests = new List<FeedbackRequest>() {
            // First Resolution
            new FeedbackRequest() {
                FeedbackRequestId=0,
                CreationDate=DateTime.Now,
                ResolutionId=0,
                Description="This sounds like a good proposal",
                ESignature="bob jones",
                Resolved=true,
                Accepted=false,
                OwnerUserID="37c1ba03-d67c-437e-ac19-2b38b123c55a"
            },
            new FeedbackRequest() {
                FeedbackRequestId=1,
                CreationDate=DateTime.Now,
                ResolutionId=0,
                Description="This sounds like a good proposal",
                ESignature="bob jones",
                Resolved=true,
                Accepted=true,
                OwnerUserID="221fedc9-3ad4-492e-bfc0-20f198923a24"
            },
            new FeedbackRequest() {
                FeedbackRequestId=2,
                CreationDate=DateTime.Now,
                ResolutionId=0,
                Description="I have this proposal",
                ESignature="jane doe",
                Resolved=true,
                Accepted=false,
                OwnerUserID="d34e5684-030b-4bf1-ba0b-51c424468294"
            },
            new FeedbackRequest() {
                FeedbackRequestId=3,
                CreationDate=DateTime.Now,
                ResolutionId=0,
                Description="Sounds good",
                ESignature="Shawn Kal",
                Resolved=true,
                Accepted=true,
                OwnerUserID="c5955b95-5492-4c7b-a3cb-c749c85e3a16"
            },
            new FeedbackRequest() {
                FeedbackRequestId=4,
                CreationDate=DateTime.Now,
                ResolutionId=0,
                Description="i like this",
                ESignature="Emily Low",
                Resolved=true,
                Accepted=true,
                OwnerUserID="5559d343-5062-4cd1-b0ae-25301e70a10d"
            },
            // Second Resolution
            new FeedbackRequest() {
                FeedbackRequestId=5,
                CreationDate=DateTime.Now,
                ResolutionId=1,
                Description="",
                ESignature="",
                Resolved=false,
                Accepted=false,
                OwnerUserID="37c1ba03-d67c-437e-ac19-2b38b123c55a"
            },
            new FeedbackRequest() {
                FeedbackRequestId=6,
                CreationDate=DateTime.Now,
                ResolutionId=1,
                Description="",
                ESignature="",
                Resolved=false,
                Accepted=false,
                OwnerUserID="221fedc9-3ad4-492e-bfc0-20f198923a24"
            },
            new FeedbackRequest() {
                FeedbackRequestId=7,
                CreationDate=DateTime.Now,
                ResolutionId=1,
                Description="",
                ESignature="",
                Resolved=false,
                Accepted=false,
                OwnerUserID="d34e5684-030b-4bf1-ba0b-51c424468294"
            },
            new FeedbackRequest() {
                FeedbackRequestId=8,
                CreationDate=DateTime.Now,
                ResolutionId=1,
                Description="",
                ESignature="",
                Resolved=false,
                Accepted=false,
                OwnerUserID="c5955b95-5492-4c7b-a3cb-c749c85e3a16"
            },
            new FeedbackRequest() {
                FeedbackRequestId=9,
                CreationDate=DateTime.Now,
                ResolutionId=1,
                Description="",
                ESignature="",
                Resolved=false,
                Accepted=false,
                OwnerUserID="5559d343-5062-4cd1-b0ae-25301e70a10d"
            },
            // Third Resolution
            new FeedbackRequest() {
                FeedbackRequestId=10,
                CreationDate=DateTime.Now,
                ResolutionId=2,
                Description="This sounds like a good proposal",
                ESignature="bob jones",
                Resolved=true,
                Accepted=true,
                OwnerUserID="37c1ba03-d67c-437e-ac19-2b38b123c55a"
            },
            new FeedbackRequest() {
                FeedbackRequestId=11,
                CreationDate=DateTime.Now,
                ResolutionId=2,
                Description="This sounds like a good proposal",
                ESignature="bob jones",
                Resolved=true,
                Accepted=true,
                OwnerUserID="221fedc9-3ad4-492e-bfc0-20f198923a24"
            },
            new FeedbackRequest() {
                FeedbackRequestId=12,
                CreationDate=DateTime.Now,
                ResolutionId=2,
                Description="I hate this proposal",
                ESignature="jane doe",
                Resolved=true,
                Accepted=false,
                OwnerUserID="d34e5684-030b-4bf1-ba0b-51c424468294"
            },
            new FeedbackRequest() {
                FeedbackRequestId=13,
                CreationDate=DateTime.Now,
                ResolutionId=2,
                Description="",
                ESignature="",
                Resolved=false,
                Accepted=false,
                OwnerUserID="c5955b95-5492-4c7b-a3cb-c749c85e3a16"
            },
            new FeedbackRequest() {
                FeedbackRequestId=14,
                CreationDate=DateTime.Now,
                ResolutionId=2,
                Description="",
                ESignature="",
                Resolved=false,
                Accepted=false,
                OwnerUserID="5559d343-5062-4cd1-b0ae-25301e70a10d"
            },
            // Fourth Resolution
            new FeedbackRequest() {
                FeedbackRequestId=15,
                CreationDate=DateTime.Now,
                ResolutionId=3,
                Description="",
                ESignature="",
                Resolved=false,
                Accepted=false,
                OwnerUserID="37c1ba03-d67c-437e-ac19-2b38b123c55a"
            },
            new FeedbackRequest() {
                FeedbackRequestId=16,
                CreationDate=DateTime.Now,
                ResolutionId=3,
                Description="",
                ESignature="",
                Resolved=false,
                Accepted=false,
                OwnerUserID="221fedc9-3ad4-492e-bfc0-20f198923a24"
            },
            new FeedbackRequest() {
                FeedbackRequestId=17,
                CreationDate=DateTime.Now,
                ResolutionId=3,
                Description="",
                ESignature="",
                Resolved=false,
                Accepted=false,
                OwnerUserID="d34e5684-030b-4bf1-ba0b-51c424468294"
            },
            new FeedbackRequest() {
                FeedbackRequestId=18,
                CreationDate=DateTime.Now,
                ResolutionId=3,
                Description="",
                ESignature="",
                Resolved=false,
                Accepted=false,
                OwnerUserID="c5955b95-5492-4c7b-a3cb-c749c85e3a16"
            },
            new FeedbackRequest() {
                FeedbackRequestId=19,
                CreationDate=DateTime.Now,
                ResolutionId=3,
                Description="",
                ESignature="",
                Resolved=false,
                Accepted=false,
                OwnerUserID="5559d343-5062-4cd1-b0ae-25301e70a10d"
            },
        };
            return feedbackRequests;
        }
    }
}